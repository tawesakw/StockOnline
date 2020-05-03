using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Apitron.PDF.Kit;
using Apitron.PDF.Kit.Styles;
using Thickness = Apitron.PDF.Kit.Styles.Thickness;
using Style = Apitron.PDF.Kit.Styles.Style;
using Font = Apitron.PDF.Kit.Styles.Text.Font;
using Apitron.PDF.Kit.FlowLayout.Content.Controls;

using Apitron.PDF.Kit.FixedLayout.Resources;
using Apitron.PDF.Kit.Interactive.Forms;
using Apitron.PDF.Kit.FlowLayout.Content;
using Apitron.PDF.Kit.Styles.Appearance;
using StockOnline.Common;

namespace StockOnline { 

    public partial class ViewOrder : ContentPage
    {

        Stream documentStream;

        public ViewOrder()
        {
            InitializeComponent();
            string fileName =  "StockOnline.Assets.GIS Succinctly.pdf";
            //string fileName = printPDF();
            documentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream(fileName);

            pdfView.LoadDocument(documentStream);
            printButton.Clicked += PrintButton_Clicked;
        }

        private void PrintButton_Clicked(object sender, EventArgs e)
        {
            Stream printStream = pdfView.SaveDocument();
            DependencyService.Get<IPrintService>().Print(printStream, "GIS Succinctly.pdf");
        }
        /*

       private string printPdfFile() {
            string returnValue = "";
            try
            {
                String fileName = "Order" + DateTime.Now.ToString("ddMMyyyy");
                // System.IO.FileStream fs = new FileStream(Server.MapPath("pdf") + "\\" + "First PDF document.pdf", FileMode.Create);

                System.IO.FileStream fs = new FileStream(Environment.GetEnvironmentVariable("Assets") + "Order.pdf", FileMode.Create);
                // Create an instance of the document class which represents the PDF document itself.
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                // Create an instance to the PDF file by creating an instance of the PDF Writer class, using the document and the filestrem in the constructor.

                PdfWriter writer = PdfWriter.GetInstance(document, fs);


                // Open the document to enable you to write to the document

                document.Open();

                // Add a simple and well known phrase to the document in a flow layout manner

                document.Add(new Paragraph("Hello World!"));

                // Close the document

                document.Close();
                // Close the writer instance

                writer.Close();
                // Always close open file handles explicitly
                fs.Close();
                returnValue = fileName;
            }
            catch (Exception ee) {
                returnValue = "ERROR" + ee.Message;
            }
            return returnValue;
        }
      */


        private string printPdfFile()
        {
            string returnValue = "";
            try
            {
                string fileName = "";

                // create flow document and register necessary styles
                FlowDocument doc = new FlowDocument();
                doc.Margin = new Thickness(10, 10, 10, 10);
                // the style for all document's textblocks
                doc.StyleManager.RegisterStyle("TextBlock, TextBox", new Style()
                {
                    Font = new Font("Helvetica", 20),
                    Color = RgbColors.Black,
                    Display = Display.Block
                });

                // the style for the section that contains employee data
                doc.StyleManager.RegisterStyle("#border", new Style()
                {
                    Padding = new Thickness(10, 10, 10, 10),
                    BorderColor = RgbColors.DarkRed,
                    Border = new Border(5),
                    BorderRadius = 5
                }
                );

                // add PDF form fields for later processing
                doc.Fields.Add(new TextField("firstName", "Hello"));
                doc.Fields.Add(new TextField("lastName", "test12345"));
                doc.Fields.Add(new TextField("position", "Manager"));

                // create section and add text block inside
                Section section = new Section() { Id = "border" };

                //  ios PDF preview doesn't display fields correctly, 
                //  uncomment this code to use simple text blocks instead of text boxes	  		  
                //			section.Add(new TextBlock(string.Format("First name: {0}",currentEmployee.FirstName)));
                //		    section.Add(new TextBlock(string.Format("Last name: {0}",currentEmployee.LastName)));
                //		    section.Add(new TextBlock(string.Format("Position: {0}",currentEmployee.CurrentPosition)));

                section.Add(new TextBlock("First name: "));
                section.Add(new TextBox("firstName"));
                section.Add(new TextBlock("Last name: "));
                section.Add(new TextBox("lastName"));
                section.Add(new TextBlock("Position: "));
                section.Add(new TextBox("position"));

                doc.Add(section);

                // get io service and generate output file path
                var fileManager = DependencyService.Get<IFileIO>();
                //string filePath = Path.Combine(fileManager.GetMyDocumentsPath(), "form.pdf");
                string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "form.pdf");

                //System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)


                // generate document
                using (Stream outputStream = fileManager.CreateFile(filePath))
                {
                    doc.Write(outputStream, new ResourceManager());
                }

                // request preview
                DependencyService.Get<IPDFPreviewProvider>().TriggerPreview(filePath);



                returnValue = fileName;
            }
            catch (Exception e)
            {
                returnValue = e.Message;
            }

            return returnValue;
        }
        
        }
}