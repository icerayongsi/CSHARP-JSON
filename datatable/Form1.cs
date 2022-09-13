using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace datatable
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
            
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

     

        public static DataTable ConvertJsonToDatatableLinq(string jsonString)
        {
            var jsonLinq = JObject.Parse(jsonString);

            var linqArray = jsonLinq.Descendants().Where(x => x is JArray).First();
            var jsonArray = new JArray();
            foreach (JObject row in linqArray.Children<JObject>())
            {
                var createRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    
                    if (column.Value is JValue)
                        createRow.Add(column.Name, column.Value);
                }
                jsonArray.Add(createRow);
                
            }

            return JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string strJSON = @"{Employees:[
              {
                EmployeeID: ""1"",
                EmployeeName: ""Dhruv""
              },
              {
                EmployeeID: ""2"",
                EmployeeName: ""Bhavin""
              },
              {
                EmployeeID: ""4"",
                EmployeeName: ""Arvind""
              },
              {
                EmployeeID: ""5"",
                EmployeeName: ""Aditya""
              },
              {
                EmployeeID: ""6"",
                EmployeeName: ""Vidhi""
              },
              {
                EmployeeID: ""7"",
                EmployeeName: ""Neha""
              }
            ]}";

            DataTable dtNewtonSoftLinq = ConvertJsonToDatatableLinq(strJSON);


            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            {
                button.Name = "button";
                button.HeaderText = "Button";
                button.Text = "Button";
                button.UseColumnTextForButtonValue = true;
            }

            dataGridView1.Columns.Add(button);

            dataGridView1.DataSource = dtNewtonSoftLinq;

        }
    }
}
