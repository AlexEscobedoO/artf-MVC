using iTextSharp.text;
using iTextSharp.text.pdf;

namespace artf_MVC.Helper.Constancias
{
    public class Footer : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // Configurar el color dorado
            BaseColor dorado = new BaseColor(255, 215, 0);

            // Configurar la fuente y el tamaño del texto
            Font font = FontFactory.GetFont(FontFactory.HELVETICA, 8f, Font.NORMAL, dorado);

            // Crear el párrafo con la información del pie de página
            Paragraph footer = new Paragraph("Avenida Universidad #1738, Planta Baja, Colonia Barrio Santa Catarina, Alcaldía Coyoacán, C.P. 04010, Ciudad de México | teléfono: 57239300 Ext. 73400 | artf@sct.gob.mx", font);

            // Alinear el párrafo al centro
            footer.Alignment = Element.ALIGN_CENTER;

            // Agregar el párrafo al documento
            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            table.AddCell(new PdfPCell(footer) { Border = 0 });

            // Posicionar la tabla en la parte inferior de la página
            table.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);

            base.OnEndPage(writer, document);
        }
    }

    class Program
    {
        static void Main()
        {
            using (Document document = new Document())
            {
                // Ruta de salida del archivo PDF
                string outputPath = "output.pdf";

                // Crear el escritor PDF
                using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                {
                    using (PdfWriter writer = PdfWriter.GetInstance(document, fs))
                    {
                        // Agregar el evento del pie de página
                        writer.PageEvent = new Footer();

                        // Abrir el documento para escribir
                        document.Open();

                        // Agregar contenido al documento (aquí deberías agregar tus tablas y otros elementos)

                        // Cerrar el documento
                        document.Close();
                    }
                }
            }
        }
    }
}