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
using System.Windows.Shapes;
using TourismManagementSystem.Model;
using System.Configuration;

namespace TourismManagementSystem.View
{
    /// <summary>
    /// Interaction logic for AddLocationWindow.xaml
    /// </summary>
    public partial class AddLocationWindow : Window

    {
        public AddLocationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                       
            try
            {


                var dataProvider = DataProvider.Ins;
                using (var dbContext = dataProvider.DB)
                {
                    dbContext.Database.Connection.Open();
                    
                    var newLocation = new DIADIEM()
                    {
                        MADD = tbMaDD.Text,
                        TENDD = tbTenDD.Text,
                        DIACHI = tbDc.Text,
                        MOTA = tbMota.Text,
                    };

                    dbContext.DIADIEMs.Add(newLocation);
                    dbContext.SaveChanges();

                    MessageBox.Show("Location added successfully!");

                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            

        }
        private void getData()
        {
           
            try
            {
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            
        }



    }
}