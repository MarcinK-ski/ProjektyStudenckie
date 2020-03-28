using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace PT_Lab1
{
    class FilesManagement
    {
        public const long MAX_FILE_SIZE_TO_OPEN_IN_MEGABYTES = 10;

        public StructureItem GetDiskTreeStructure(string disk)
        {
            DriveInfo driveInfo = new DriveInfo(disk);
            DirectoryItem structureItem = new DirectoryItem(driveInfo.Name.ToUpper(), driveInfo.Name, RefreshChildItemsStructure);

            if (driveInfo.IsReady)
            {
                DirectoryInfo rootDirectoryInfo = driveInfo.RootDirectory;
                AddElementsToStructure(rootDirectoryInfo, structureItem);
            }
            else
            {
                Console.WriteLine($"Disk {disk} could not be read");
            }

            return structureItem;
        }

        private void RefreshChildItemsStructure(DirectoryItem structureItem)
        {
            DirectoryInfo root = new DirectoryInfo(structureItem.Path);
            AddElementsToStructure(root, structureItem);
        }

        public void AddElementsToStructure(DirectoryInfo root, DirectoryItem structureItem)
        {
            WalkThroughTree(root, structureItem, false);
        }

        private void WalkThroughTree(DirectoryInfo root, DirectoryItem structureItem, bool digDown)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subdirs = null;

            try
            {
                files = root.GetFiles();
                subdirs = root.GetDirectories();
                structureItem.PermissionToItem = PermissionStatus.P_GRANDED;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
                structureItem.PermissionToItem = PermissionStatus.P_DENIED;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            structureItem.ClearItemsCollection();

            if (subdirs != null)
            {
                foreach (DirectoryInfo directoryInfo in subdirs)
                {
                    DirectoryItem currentStructureItem = new DirectoryItem(directoryInfo.Name, directoryInfo.FullName, RefreshChildItemsStructure)
                    {
                        Attributes = directoryInfo.Attributes,
                        Parent = structureItem
                    };

                    structureItem.AddNewItem(currentStructureItem);

                    if (digDown)
                    {
                        WalkThroughTree(directoryInfo, currentStructureItem, true);
                    }
                }
            }

            if (files != null)
            {
                foreach (FileInfo fileInfo in files)
                {
                    FileItem currentStructureItem = new FileItem(fileInfo.Name, fileInfo.FullName)
                    {
                        Attributes = fileInfo.Attributes,
                        FileSize = fileInfo.Length,
                        Parent = structureItem
                    };

                    structureItem.AddNewItem(currentStructureItem);
                }
            }
        }

        public static string[] GetDrives()
        {
            return Array.ConvertAll(DriveInfo.GetDrives(), d => d.ToString());
        }

        public static void CreateNewElement(StructureItem structureItem, string name, ElementType createElementType)
        {
            string path = String.Empty;

            if (structureItem is FileItem)
            {
                // TODO: Get directory
            }
            else if (structureItem is DirectoryItem)
            {
                path = $"{structureItem.Path}\\{name}";
            }

            if(!String.IsNullOrEmpty(path))
            {
                if (createElementType == ElementType.DIRECTORY)
                {
                    Directory.CreateDirectory(path);
                }
                else if (createElementType == ElementType.FILE)
                {
                    File.Create(path);
                }
            }
        }

        public static void RemoveElement(StructureItem structureItem)
        {
            if (structureItem is FileItem)
            {
                File.Delete(structureItem.Path);
            }
            else if (structureItem is DirectoryItem)
            {
                Directory.Delete(structureItem.Path);
            }
        }

        public static async Task<string> GetFileContent(FileItem fileItem)
        {
            long fileSize = fileItem.GetFileSizeInMegabytes();
            if (fileSize < MAX_FILE_SIZE_TO_OPEN_IN_MEGABYTES)
            {
                return await GetFileContent(fileItem.Path);
            }

            throw new TooLargeFileException(fileSize, "MB", MAX_FILE_SIZE_TO_OPEN_IN_MEGABYTES, "MB");
        }

        private static async Task<string> GetFileContent(string path)
        {
            using (StreamReader streamReader = File.OpenText(path))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}
