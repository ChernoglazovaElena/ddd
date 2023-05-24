using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            JsonDS.SeartchPathJsonFiles();
            Calendar.Text = Calendar.DisplayDate.ToString();
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int a = 0;
                if (JsonDS.Deserialize<List<Note>>("Note.json") != null)
                {
                    ListNoteDG.ItemsSource = JsonDS.Deserialize<List<Note>>("Note.json").Where(item => item.Date == Calendar.Text);

                    foreach(Note element in ListNoteDG.Items)
                    {
                        a = element.Add ?a + element.Price:a - element.Price;
                    }
                    FinalPrice.Text = "Итог: "+a;
                }
                else
                {
                    FinalPrice.Text = "Итог: 0";
                }
            }
            catch {};
        }
        private void TypeOfNoteCB_Loaded(object sender, RoutedEventArgs e)
        {
            TypeOfNoteCB.ItemsSource = JsonDS.Deserialize<List<string>>("TypeOfNote.json");
        }
        private void CreateNewTypeNoteB_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Show();
            window.Closed += Window_Closed;
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as Window1).Text))
            {
                List<string> st = new List<string>();
                if ((List<string>)TypeOfNoteCB.ItemsSource == null)
                {
                    st.Add((sender as Window1).Text);
                }
                else
                {
                    st = (List<string>)TypeOfNoteCB.ItemsSource;
                    for (int i =0;i < st.Count(); i++)
                    {
                        if (st[i] == (sender as Window1).Text)
                        {
                            break;
                        }
                        if (i == st.Count-1)
                        {
                            st.Add((sender as Window1).Text);
                        }
                    }
                }
                JsonDS.Serialize("TypeOfNote.json", st);
                TypeOfNoteCB_Loaded(null, null);
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameNoteTB.Text) && StringIsAllNumber(PriceТB.Text) && !string.IsNullOrEmpty(TypeOfNoteCB.Text))
            {
                CUD.Create(new Note
                {
                    Name = NameNoteTB.Text,
                    TypeOf = TypeOfNoteCB.Text,
                    Price = Convert.ToInt32(PriceТB.Text),
                    Date = DateTime.Now.ToString("D")
                });
                DatePicker_SelectedDateChanged(null, null);
                ClearTextBlock();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Note note = (Note)ListNoteDG.Items[ListNoteDG.SelectedIndex];
            string NoUpdatedName = note.Name;
            note.Name = NameNoteTB.Text;
            note.Price = Convert.ToInt32(PriceТB.Text);
            note.TypeOf = TypeOfNoteCB.Text;
            CUD.Update(note, NoUpdatedName, Calendar.Text);
            DatePicker_SelectedDateChanged(null, null);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Note note = (Note)ListNoteDG.Items[ListNoteDG.SelectedIndex];
            CUD.Delete(note);
            ClearTextBlock();
            DatePicker_SelectedDateChanged(null, null);
        }

        private void ListNoteDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Delete.IsEnabled = true;
            Update.IsEnabled = true;
            Read((Note)ListNoteDG.Items[ListNoteDG.SelectedIndex]);
        }
        private void Read(Note note)
        {
            NameNoteTB.Text = note.Name;
            TypeOfNoteCB.Text = note.TypeOf;
            PriceТB.Text = (note.Price).ToString();
        }
        private void ClearTextBlock()
        {
            NameNoteTB.Text = null;
            TypeOfNoteCB.Text = null;
            PriceТB.Text = null;
        }
        private bool StringIsAllNumber(string str)
        {
            foreach(char element in str)
            {
                if (!char.IsDigit(element))
                {
                    return false;
                }
            }
            return true;
        }
    }
}