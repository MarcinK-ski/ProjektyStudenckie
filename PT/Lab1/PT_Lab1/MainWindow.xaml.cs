using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PT_Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StructureItem _structureItem;
        private FilesManagement _filesManagement;

        public MainWindow()
        {
            InitializeComponent();
            GenerateMenu();

            _filesManagement = new FilesManagement();
        }

        private void GenerateMenu()
        {
            string[] drives = FilesManagement.GetDrives();

            foreach (string drive in drives)
            {
                MenuItem menuItem = new MenuItem()
                {
                    Header = drive,
                    IsCheckable = true
                };
                menuItem.Click += MenuDriveSelectorClick;

                MenuItemCalledAsMenu.Items.Add(menuItem);
            }

            MenuItemCalledAsMenu.Items.Add(new Separator());

            MenuItem exitMenuItem = new MenuItem()
            {
                Header = "Exit"
            };
            exitMenuItem.Click += (sender, e) => App.Current.Shutdown();

            MenuItemCalledAsMenu.Items.Add(exitMenuItem);

            if (drives.Length > 0)
            {
                StatusText.Text = "SELECT DRIVE FIRST";
            }
            else
            {
                StatusText.Text = "NO DRIVE DETECTED!";
            }
        }

        private void MenuDriveSelectorClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                MenuItem clickedMenuItem = sender as MenuItem;

                Structure.Items.Clear();

                if (clickedMenuItem.IsChecked)
                {
                    try
                    {
                        _structureItem = _filesManagement.GetDiskTreeStructure(clickedMenuItem.Header.ToString());
                        Structure.Items.Add(_structureItem);
                        StatusText.Text = "Expand the tree...";
                    }
                    catch (Exception ex)
                    {
                        StatusText.Text = ex.Message;
                    }
                }
                else
                {
                    StatusText.Text = "SELECT DRIVE FIRST";
                }
            }
        }

        private async Task ChangeStatusText(string value, bool joinWithLastText = false)
        {
            StatusText.Text = joinWithLastText ? $"{StatusText.Text} {value}" : value;
            await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
        }

        private async void StructureTreeExpanded(object sender, RoutedEventArgs e)
        {

            TreeViewItem treeViewItem = (TreeViewItem)e.OriginalSource;

            if (treeViewItem.Header is StructureItem)
            {
                StructureItem currentStructureItem = treeViewItem.Header as StructureItem;

                if (currentStructureItem != null)
                {
                    if (currentStructureItem is DirectoryItem)
                    {
                        await ChangeStatusText("Loading data...");

                        DirectoryItem directoryItem = currentStructureItem as DirectoryItem;

                        directoryItem.IsChanged = true;

                        foreach (StructureItem childStructureItem in directoryItem.Items)
                        {
                            if (childStructureItem is DirectoryItem)
                            {
                                (childStructureItem as DirectoryItem).IsChanged = true;
                            }
                        }

                        await ChangeStatusText("Ready.");
                    }
                }
            }
        }

        private async void StructureSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.OriginalSource is TreeView)
            {
                StructureItem si = ((TreeView)e.OriginalSource).SelectedItem as StructureItem;

                if (si != null)
                {
                    await ChangeStatusText(si.Attributes.ToString());
                }
            }
        }

        private async void Structure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TreeView)
            {
                FileItem file = ((TreeView)e.Source).SelectedItem as FileItem;

                if (file != null)
                {
                    await ChangeStatusText($" Size: {file.GetFileSizeInMegabytes()} MB; Trying to open file \"{file.Title}\"... ", true);

                    try
                    {
                        string fileContent = await FilesManagement.GetFileContent(file);
                        FileContentTextBox.Text = fileContent;

                        await ChangeStatusText("Opened!", true);
                    }
                    catch (Exception ex)
                    {
                        await ChangeStatusText(ex.Message);
                    }
                }
            }
        }

        private void StructureMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TreeView)
            {
                TreeView source = e.Source as TreeView;

                // TODO: Highlight selected item (RMB)
            }
        }

        private void MenuItemCreateNewClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem clickedMenuItem = sender as MenuItem;

                StructureItem structureItem = clickedMenuItem.DataContext as StructureItem;

                // TODO: Delete mock and add window creator
                FilesManagement.CreateNewElement(structureItem, "TESTOWY", ElementType.DIRECTORY);
                FilesManagement.CreateNewElement(structureItem, "TESTOWY.txt", ElementType.FILE);
                (structureItem as DirectoryItem).IsChanged = true;

            }
            catch (Exception ex)
            {
                StatusText.Text = $"Problem while creating element. {ex.Message}";
            }
        }

        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem clickedMenuItem = sender as MenuItem;

                StructureItem structureItem = clickedMenuItem.DataContext as StructureItem;
                DirectoryItem parentDirectory = structureItem.Parent;
                FilesManagement.RemoveElement(structureItem);
                if (parentDirectory != null)
                {
                    parentDirectory.IsChanged = true;
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Problem while deleting. {ex.Message}";
            }
        }
    }
}
