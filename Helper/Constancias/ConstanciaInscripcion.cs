﻿using artf_MVC.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;

namespace artf_MVC.Helper.Constancias
{
    public class ConstanciaInscripcion
    {
        public IActionResult Generar(Equiuni equiuni)
        {
            using (var pdfStream = new MemoryStream())
            {
                string projectDirectory = Directory.GetCurrentDirectory();

                //Se obtienen las rutas de los logos dentro del proyecto para la constancia
                string imagePath1 = Path.Combine(projectDirectory, "Helper", "Resources", "SCT LOGO.png");
                string imagePath2 = Path.Combine(projectDirectory, "Helper", "Resources", "ARTF.png");
                string marcadeagua = Path.Combine(projectDirectory, "Helper", "Resources", "FONDO.png");


                // Tamaño carta
                Document pdfDocument = new Document(PageSize.A4);
                pdfDocument.SetMargins(36f, 36f, 12f, 36f); // Establecer márgenes en puntos (1 pulgada)

                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, pdfStream);
                pdfWriter.PageEvent = new Footer();

                pdfDocument.Open();

                PdfPTable headerTable = new PdfPTable(2);
                headerTable.TotalWidth = pdfDocument.PageSize.Width - pdfDocument.LeftMargin - pdfDocument.RightMargin;
                //Se agrega la primera imagen del encabezado
                PdfPCell cell1 = new PdfPCell();
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.Border = PdfPCell.NO_BORDER;
                Image image1 = Image.GetInstance(imagePath1);
                image1.ScaleToFit(150f, 100f);
                cell1.AddElement(image1);
                headerTable.AddCell(cell1);
                //Se agrega la segunda imagen del encabezado
                PdfPCell cell2 = new PdfPCell();
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.Border = PdfPCell.NO_BORDER;
                Image image2 = Image.GetInstance(imagePath2);
                image2.ScaleToFit(150f, 100f);
                cell2.AddElement(image2);
                headerTable.AddCell(cell2);

                pdfDocument.Add(headerTable);

                //----------------------------------------- Crear un TITULO centrado y justificado------------------------------------------/
                Paragraph titleParagraph = new Paragraph("CONSTANCIA DE INSCRIPCIÓN DE EQUIPO FERROVIARIO\nEN EL REGISTRO FERROVIARIO MEXICANO",
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK));
                titleParagraph.Alignment = Element.ALIGN_CENTER;
                titleParagraph.SetLeading(10, 0); // Ajustar el espaciado entre líneas si es necesario
                pdfDocument.Add(titleParagraph);
                //-------------------------------------------------------------------------------------------------------------------------------//
                //Salto de linea
                pdfDocument.Add(new Paragraph("\n"));

                // Crear la tabla
                PdfPTable table = new PdfPTable(1); // Número de columnas, ajusta según tus necesidades

                // Configurar el borde de la tabla principal
                table.DefaultCell.BorderWidthBottom = 1f;
                table.DefaultCell.BorderWidthTop = 1f;
                table.DefaultCell.BorderWidthLeft = 1f;
                table.DefaultCell.BorderWidthRight = 1f;

                // Ajustar el ancho de la tabla al 100% del ancho de la página
                table.WidthPercentage = 100;

                // Añadir celda de título a la tabla
                PdfPCell titleCell = new PdfPCell(new Phrase("INFORMACIÓN DEL EQUIPO FERROVIARIO"));
                titleCell.Colspan = 4; // Ocupa todas las columnas
                titleCell.BackgroundColor = new BaseColor(192, 192, 192); // Fondo gris
                titleCell.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                table.AddCell(titleCell);

                // Agregar una fila vacía para simular el espacio entre el encabezado y la celda
                float espacioEntreEncabezadoYCell = 9f; // Ajusta según sea necesario
                PdfPCell emptyCell = new PdfPCell(new Phrase(""));
                emptyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Poner el borde izquierdo y derecho
                emptyCell.FixedHeight = espacioEntreEncabezadoYCell;
                table.AddCell(emptyCell);

                // ROW (TRACTIVO, ARRASTRE, TRABAJO)
                PdfPCell cell3 = new PdfPCell(CreateCustomSubTable1(equiuni.Modaequi));
                cell3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Poner el borde izquierdo y derecho
                table.AddCell(cell3);

                // ROW (MATRICULA, FECHA CONSTRUCCION)
                PdfPCell cell4 = new PdfPCell(CreateCustomSubTable2(equiuni.Fcons));
                cell4.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Poner el borde izquierdo y derecho
                table.AddCell(cell4);

                //ROW (MODELO Y TIPO DE EQUIPO)
                PdfPCell cell5 = new PdfPCell(CreateCustomSubTable3(equiuni.IdmodeequiNavigation.Modequi, equiuni.Tipequi));
                cell5.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Poner el borde izquierdo y derecho
                table.AddCell(cell5);

                //ROW (MARCA Y TIPO DE USO)
                PdfPCell cell6 = new PdfPCell(CreateCustomSubTable4(equiuni.IdfabequiNavigation.Marfab, equiuni.Usoequi));
                cell6.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Poner el borde izquierdo y derecho
                table.AddCell(cell6);

                //ROW (# SERIE)
                PdfPCell cell7 = new PdfPCell(CreateCustomSubTable5(equiuni.Nserie));
                cell7.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Poner el borde izquierdo y derecho
                table.AddCell(cell7);

                //ROW (FABRICANTE Y GRAVEM)
                PdfPCell cell8 = new PdfPCell(CreateCustomSubTable6(equiuni.IdfabequiNavigation.Rsfab, equiuni.Graequi));
                //cell8.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; // Poner el borde izquierdo, derecho y inferior
                //table.AddCell(cell8);
                Random random = new Random();

               
                if (equiuni.Graequi == "Si")
                {
                    cell8.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Poner el borde izquierdo, derecho y inferior
                    table.AddCell(cell8);

                    //ROW (DESCRIPCION DEL GRAVAMEN)
                    PdfPCell cell9 = new PdfPCell(CreateCustomSubTable7(equiuni.Obsgra));
                    cell9.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; // Poner el borde izquierdo y derecho
                    table.AddCell(cell9);

                }
                else
                {
                    cell8.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; // Poner el borde izquierdo, derecho y inferior
                    table.AddCell(cell8);
                }


                // Añadir la tabla al documento
                pdfDocument.Add(table);

                /*-----------------------------------------------------------------------------------------------------TABLA 2-------------------------------------------------------------------------*/
                // Crear la tabla
                PdfPTable table2 = new PdfPTable(1); // Número de columnas, ajusta según tus necesidades

                // Configurar el borde de la tabla principal
                table2.DefaultCell.BorderWidthBottom = 1f;
                table2.DefaultCell.BorderWidthTop = 1f;
                table2.DefaultCell.BorderWidthLeft = 1f;
                table2.DefaultCell.BorderWidthRight = 1f;

                // Ajustar el ancho de la tabla al 100% del ancho de la página
                table2.WidthPercentage = 100;

                // Añadir celda de título
                PdfPCell titleCellTable2 = new PdfPCell(new Phrase("INFORMACIÓN DEL PROPIETARIO Y USUARIO"));
                titleCellTable2.Colspan = 4; // Ocupa todas las columnas
                titleCellTable2.BackgroundColor = new BaseColor(192, 192, 192); // Fondo gris
                titleCellTable2.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                table2.AddCell(titleCellTable2);

                // Agregar una fila vacía para simular el espacio entre el encabezado y la celda
                float espacioEntreEncabezadoYCellTable2 = 9f; // Ajusta según sea necesario
                PdfPCell emptyCellTable2 = new PdfPCell(new Phrase(""));
                emptyCellTable2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                emptyCellTable2.FixedHeight = espacioEntreEncabezadoYCellTable2;
                table2.AddCell(emptyCellTable2);

                // ROW (PROPIO, RENTADO)
                PdfPCell cell3Table2 = new PdfPCell(CreateCustomSubTable2_1(equiuni.Regiequi));
                cell3Table2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table2.AddCell(cell3Table2);

                // ROW (CONTRATO, FECHA CONTRATO)
                PdfPCell cell4Table2 = new PdfPCell(CreateCustomSubTable2_2(equiuni.Tcontra, equiuni.Fcontra.Value.Date.ToString()));
                cell4Table2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table2.AddCell(cell4Table2);

                // ROW (NOMBRE DEL PROPIETARIO)
                PdfPCell cell5Table2 = new PdfPCell(CreateCustomSubTable2_3(equiuni.IdempreequiNavigation.Rsempre));
                cell5Table2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table2.AddCell(cell5Table2);

                // ROW (NOMBRE DEL USUARIO)
                PdfPCell cell6Table2 = new PdfPCell(CreateCustomSubTable2_4(equiuni.IdempreequiNavigation.Rsempre));
                cell6Table2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table2.AddCell(cell6Table2);

                // ROW (DESCRIPCION)
                PdfPCell cell7Table2 = new PdfPCell(CreateCustomSubTable2_5(equiuni.Obsequi));
                cell7Table2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table2.AddCell(cell7Table2);

                // ROW (OBSERVACIONES)
                PdfPCell cell8Table2 = new PdfPCell(CreateCustomSubTable2_6(equiuni.IdinsequiNavigation.Obsins));
                cell8Table2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table2.AddCell(cell8Table2);

                // ROW (NO OFICIO Y FECHA OFICIO)
                PdfPCell cell9Table2 = new PdfPCell(CreateCustomSubTable2_7(equiuni.IdinsequiNavigation.Numacuofins, equiuni.IdinsequiNavigation.Fecapins.ToString()));
                cell9Table2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; // Quital el borde izquierdo y derecho
                table2.AddCell(cell9Table2);

                // Añadir la tabla al documento
                pdfDocument.Add(table2);

                /*------------------------------------------------------------Fin tabla 2-------------------------------------------------------------------------------------------------------------------------*/
                // Crear la tabla 3
                PdfPTable table3 = new PdfPTable(1); // Número de columnas, ajusta según tus necesidades

                // Configurar el borde de la tabla principal
                table3.DefaultCell.BorderWidthBottom = 1f;
                table3.DefaultCell.BorderWidthTop = 1f;
                table3.DefaultCell.BorderWidthLeft = 1f;
                table3.DefaultCell.BorderWidthRight = 1f;

                // Ajustar el ancho de la tabla al 100% del ancho de la página
                table3.WidthPercentage = 100;

                // Añadir celda de título
                PdfPCell titleCellTable3 = new PdfPCell(new Phrase("DATOS DEL REGISTRO FERROVIARIO MEXICANO"));
                titleCellTable3.Colspan = 4; // Ocupa todas las columnas
                titleCellTable3.BackgroundColor = new BaseColor(192, 192, 192); // Fondo gris
                titleCellTable3.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                table3.AddCell(titleCellTable3);

                // Agregar una fila vacía para simular el espacio entre el encabezado y la celda
                float espacioEntreEncabezadoYCellTable3 = 9f; // Ajusta según sea necesario
                PdfPCell emptyCellTable3 = new PdfPCell(new Phrase(""));
                emptyCellTable3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                emptyCellTable3.FixedHeight = espacioEntreEncabezadoYCellTable3;
                table3.AddCell(emptyCellTable3);



                // ROW (CON FOLIO)
                PdfPCell cell1Table3 = new PdfPCell(CreateCustomSubTable3_1(equiuni.Fecharequi.ToString()));
                cell1Table3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table3.AddCell(cell1Table3);
                // ROW (SELLO DIGITAL)
                PdfPCell cell2Table3 = new PdfPCell(CreateCustomSubTable3_2(equiuni.Fecharequi.ToString()));
                cell2Table3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; // Quital el borde izquierdo y derecho
                table3.AddCell(cell2Table3);
                // ROW (RFM)
                PdfPCell cell3Table3 = new PdfPCell(CreateCustomSubTable3_3());
                cell3Table3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; // Quital el borde izquierdo y derecho
                table3.AddCell(cell3Table3);


                // Añadir la tabla al documento
                pdfDocument.Add(table3);

                /**--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

                //Marca de agua

                // Cargar la imagen de la marca de agua
                Image watermarkImage = Image.GetInstance(marcadeagua);

                // Hacer la imagen un cuarto del tamaño original
                watermarkImage.ScaleAbsolute(watermarkImage.Width / 2, watermarkImage.Height / 2);

                // Ajustar la posición de la imagen en la esquina inferior derecha
                float xPosition = PageSize.A4.Width - watermarkImage.ScaledWidth - 20; // 20 es un margen
                float yPosition = 20; // 20 es un margen
                watermarkImage.SetAbsolutePosition(xPosition, yPosition);

                // Agregar la imagen como marca de agua
                PdfContentByte cb = pdfWriter.DirectContentUnder;
                cb.AddImage(watermarkImage);

                pdfDocument.Close();

                string fileName = $"Constancia_{equiuni.Idequi}.pdf";
                string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string filePath = Path.Combine(downloadsFolder, fileName);

                System.IO.File.WriteAllBytes(filePath, pdfStream.ToArray());

                return new FileStreamResult(new FileStream(filePath, FileMode.Open), "application/pdf")
                {
                    FileDownloadName = fileName
                };

            }
            /*************************************************************************INICIO TABLA 1*************************************************************************/

            // Función para crear subtabla personalizada en la tabla 1 (Columnas Tractivo, Arrastre y Trabajo)
            static PdfPTable CreateCustomSubTable1(string tipo)
            {
                PdfPTable subTable = new PdfPTable(3);

                /*****COLUMNA TRACTIVO*****/
                PdfPCell cell1 = new PdfPCell();
                cell1.Border = Rectangle.NO_BORDER; // Quital el borde
                cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                PdfPTable innerTable = new PdfPTable(2);
                innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                // Agregar el texto "Tractivo"
                PdfPCell textCell = new PdfPCell(new Phrase("Tractivo", new Font(Font.FontFamily.HELVETICA, 9f)));
                textCell.Border = Rectangle.NO_BORDER; // Quital el borde
                innerTable.AddCell(textCell);
                if (tipo == "Tractivo")
                {
                    PdfPCell rectangleCell1 = new PdfPCell(new Phrase("X", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK }));
                    rectangleCell1.BackgroundColor = new BaseColor(192, 192, 192); // Fondo gris
                    rectangleCell1.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    rectangleCell1.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCell1.VerticalAlignment = Element.ALIGN_CENTER;
                    innerTable.AddCell(rectangleCell1);
                    cell1.AddElement(innerTable);
                    subTable.AddCell(cell1);
                }
                else
                {
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell = new PdfPCell();
                    rectangleCell.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCell.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    innerTable.AddCell(rectangleCell);
                    cell1.AddElement(innerTable);
                    subTable.AddCell(cell1);
                }




                /*****COLUMNA ARRASTRE*****/
                PdfPCell cell2 = new PdfPCell();
                cell2.Border = Rectangle.NO_BORDER; // Quita el borde
                cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                                                                  // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                PdfPTable innerTable2 = new PdfPTable(2);
                innerTable2.DefaultCell.Border = Rectangle.NO_BORDER;
                // Agregar el texto "Arrastre"
                PdfPCell textCell2 = new PdfPCell(new Phrase("Arrastre", new Font(Font.FontFamily.HELVETICA, 9f)));
                textCell2.Border = Rectangle.NO_BORDER; // Quita el borde
                innerTable2.AddCell(textCell2);
                if (tipo == "Arrastre")
                {
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell2 = new PdfPCell(new Phrase("X", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK }));
                    rectangleCell2.BackgroundColor = new BaseColor(192, 192, 192); // Fondo gris
                    rectangleCell2.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    rectangleCell2.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCell2.VerticalAlignment = Element.ALIGN_CENTER;
                    innerTable2.AddCell(rectangleCell2);
                    cell2.AddElement(innerTable2);
                    subTable.AddCell(cell2);
                }
                else
                {
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell2 = new PdfPCell();
                    rectangleCell2.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCell2.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    innerTable2.AddCell(rectangleCell2);
                    cell2.AddElement(innerTable2);
                    subTable.AddCell(cell2);
                }

                // ...

                /*****COLUMNA TRABAJO*****/
                PdfPCell cell3 = new PdfPCell();
                cell3.Border = Rectangle.NO_BORDER; // Quita el borde
                cell3.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                                                                  // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                PdfPTable innerTable3 = new PdfPTable(2);
                innerTable3.DefaultCell.Border = Rectangle.NO_BORDER;
                // Agregar el texto "Trabajo"
                PdfPCell textCell3 = new PdfPCell(new Phrase("Trabajo", new Font(Font.FontFamily.HELVETICA, 9f)));
                textCell3.Border = Rectangle.NO_BORDER; // Quita el borde
                innerTable3.AddCell(textCell3);
                if (tipo == "Trabajo")
                {
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell3 = new PdfPCell(new Phrase("X", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK }));
                    rectangleCell3.BackgroundColor = new BaseColor(192, 192, 192); // Fondo gris
                    rectangleCell3.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    rectangleCell3.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCell3.VerticalAlignment = Element.ALIGN_CENTER;
                    innerTable3.AddCell(rectangleCell3);
                    cell3.AddElement(innerTable3);
                    subTable.AddCell(cell3);
                }
                else
                {
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell3 = new PdfPCell();
                    rectangleCell3.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCell3.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    innerTable3.AddCell(rectangleCell3);
                    cell3.AddElement(innerTable3);
                    subTable.AddCell(cell3);
                }

                return subTable;
            }

            // Función para crear subtabla personalizada en la tabla 1 (Matricula,Fecha construcción)
            static PdfPTable CreateCustomSubTable2(int? fechaConstruccion)
            {
                PdfPTable subTable = new PdfPTable(4);
                float[] columnWidths = new float[] { 18f, 32f, 18f, 32f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;


                cell.Phrase = new Phrase("Matrícula:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);


                Chunk textChuckValorMatricula = new Chunk("KCSM-10363", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phraseValorMatricula = new Phrase();
                phraseValorMatricula.Add(textChuckValorMatricula);
                phraseValorMatricula.Add(line);
                cell.Phrase = new Phrase(phraseValorMatricula);
                subTable.AddCell(cell);


                cell.Phrase = new Phrase("Fecha de Construcción:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);
                // Puedes agregar más celdas si es necesario

                Chunk textChuckValorFecha = new Chunk(fechaConstruccion.ToString(), new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                Phrase phraseValorFecha = new Phrase();
                phraseValorFecha.Add(textChuckValorFecha);
                phraseValorFecha.Add(line);
                cell.Phrase = new Phrase(phraseValorFecha);
                subTable.AddCell(cell);

                return subTable;

            }

            // Función para crear subtabla personalizada en la tabla 1 (Modelo,Tipo de equipo)
            static PdfPTable CreateCustomSubTable3(string Modelo, string TipoEquipo)
            {
                PdfPTable subTable = new PdfPTable(4);
                float[] columnWidths = new float[] { 18f, 32f, 18f, 32f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);



                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;


                cell.Phrase = new Phrase("Modelo:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);


                Chunk textChuckValorModelo = new Chunk(Modelo, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phraseValorModelo = new Phrase();
                phraseValorModelo.Add(textChuckValorModelo);
                phraseValorModelo.Add(line);
                cell.Phrase = new Phrase(phraseValorModelo);
                subTable.AddCell(cell);


                cell.Phrase = new Phrase("Tipo de equipo:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);
                // Puedes agregar más celdas si es necesario

                Chunk textChuckValorTipo = new Chunk(TipoEquipo, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                Phrase phraseValorTipo = new Phrase();
                phraseValorTipo.Add(textChuckValorTipo);
                phraseValorTipo.Add(line);
                cell.Phrase = new Phrase(phraseValorTipo);
                subTable.AddCell(cell);

                return subTable;
            }

            // Función para crear subtabla personalizada en la tabla 1 (Marca,Uso)
            static PdfPTable CreateCustomSubTable4(string Marca, string Uso)
            {
                PdfPTable subTable = new PdfPTable(4);
                float[] columnWidths = new float[] { 18f, 32f, 18f, 32f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);


                // Añadir contenido a las celdas de la subtabla
                PdfPCell cell1 = new PdfPCell(new Phrase("Marca:", new Font(Font.FontFamily.HELVETICA, 9f)));
                cell1.Border = Rectangle.NO_BORDER; // Quital el borde
                cell1.PaddingLeft = 9f;
                cell1.PaddingRight = 9f;
                cell1.PaddingTop = 5f;
                cell1.PaddingBottom = 5f;
                subTable.AddCell(cell1);


                PdfPCell cell2 = new PdfPCell();
                cell2.Border = Rectangle.NO_BORDER; // Quital el borde
                cell2.PaddingLeft = 9f;
                cell2.PaddingRight = 9f;
                cell2.PaddingTop = 5f;
                cell2.PaddingBottom = 5f;
                Chunk textChuckValorMarca = new Chunk(Marca, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);
                Phrase phraseValorMarca = new Phrase();
                phraseValorMarca.Add(textChuckValorMarca);
                phraseValorMarca.Add(line);
                cell2.Phrase = new Phrase(phraseValorMarca);
                subTable.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("Uso:", new Font(Font.FontFamily.HELVETICA, 9f)));
                cell3.Border = Rectangle.NO_BORDER; // Quital el borde
                cell3.PaddingLeft = 9f;
                cell3.PaddingRight = 9f;
                cell3.PaddingTop = 5f;
                cell3.PaddingBottom = 5f;
                subTable.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell();
                cell4.Border = Rectangle.NO_BORDER; // Quita el borde
                cell4.PaddingLeft = 9f;
                cell4.PaddingRight = 9f;
                cell4.PaddingTop = 5f;
                cell4.PaddingBottom = 5f;

                PdfPTable subTable4 = new PdfPTable(3);
                subTable4.DefaultCell.Border = Rectangle.NO_BORDER;
                subTable4.WidthPercentage = 100f; // O ajusta según sea necesario

                if (Uso == "Carga")
                {
                    // Primer rectángulo con texto "CARGA"
                    PdfPCell rectangleCellA = new PdfPCell(new Phrase("CARGA", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellA.BackgroundColor = new BaseColor(192, 192, 192); // Fondo blanco
                    rectangleCellA.Colspan = 1; // Ajusta según sea necesario
                    // Centrar el texto horizontalmente y verticalmente
                    rectangleCellA.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCellA.VerticalAlignment = Element.ALIGN_MIDDLE;
                    subTable4.AddCell(rectangleCellA);
                }
                else
                {
                    // Primer rectángulo con texto "CARGA"
                    PdfPCell rectangleCellA = new PdfPCell(new Phrase("CARGA", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellA.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCellA.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellA.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCellA.VerticalAlignment = Element.ALIGN_MIDDLE;
                    subTable4.AddCell(rectangleCellA);
                }

                if (Uso == "Pasajeros")
                {
                    // Segundo rectángulo con texto "PASAJEROS"
                    PdfPCell rectangleCellB = new PdfPCell(new Phrase("PASAJEROS", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellB.BackgroundColor = new BaseColor(192, 192, 192);  // Fondo blanco
                    rectangleCellB.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellB.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCellB.VerticalAlignment = Element.ALIGN_MIDDLE;
                    subTable4.AddCell(rectangleCellB);
                }
                else
                {
                    PdfPCell rectangleCellB = new PdfPCell(new Phrase("PASAJEROS", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellB.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCellB.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellB.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCellB.VerticalAlignment = Element.ALIGN_MIDDLE;
                    subTable4.AddCell(rectangleCellB);
                }


                if (Uso == "Trabajo" || Uso == "")
                {
                    // Tercer rectángulo con texto "TRABAJO"
                    PdfPCell rectangleCellC = new PdfPCell(new Phrase("TRABAJO", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellC.BackgroundColor = new BaseColor(192, 192, 192); // Fondo blanco
                    rectangleCellC.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellC.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCellC.VerticalAlignment = Element.ALIGN_MIDDLE;
                    subTable4.AddCell(rectangleCellC);
                }
                else
                {
                    // Tercer rectángulo con texto "TRABAJO"
                    PdfPCell rectangleCellC = new PdfPCell(new Phrase("TRABAJO", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellC.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCellC.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellC.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCellC.VerticalAlignment = Element.ALIGN_MIDDLE;
                    subTable4.AddCell(rectangleCellC);
                }

                // Ajustar el ancho total de la tabla según sea necesario
                subTable4.TotalWidth = 200f; // Ajusta según sea necesario

                cell4.AddElement(subTable4);

                // Agregar la cuarta celda a la subtabla principal
                subTable.AddCell(cell4);

                return subTable;
            }

            // Función para crear subtabla personalizada en la tabla 1 (No. Serie)
            static PdfPTable CreateCustomSubTable5(string NSerie)
            {
                PdfPTable subTable = new PdfPTable(2);
                float[] columnWidths = new float[] { 18f, 82f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);


                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;


                cell.Phrase = new Phrase("No. Serie:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);


                Chunk textChuck = new Chunk(NSerie, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phrase = new Phrase();
                phrase.Add(textChuck);
                phrase.Add(line);

                cell.Phrase = new Phrase(phrase);

                subTable.AddCell(cell);


                // Puedes agregar más celdas si es necesario

                return subTable;

            }
            // Función para crear subtabla personalizada en la tabla 1 (Fabricante, Gravamen)
            static PdfPTable CreateCustomSubTable6(string Fabricante, string Gravamen)
            {
                PdfPTable subTable = new PdfPTable(4);
                float[] columnWidths = new float[] { 18f, 32f, 18f, 32f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                // Añadir contenido a las celdas de la subtabla
                PdfPCell cell1 = new PdfPCell(new Phrase("Fabricante:", new Font(Font.FontFamily.HELVETICA, 9f)));
                cell1.Border = Rectangle.NO_BORDER; // Quital el borde
                cell1.PaddingLeft = 9f;
                cell1.PaddingRight = 9f;
                cell1.PaddingTop = 5f;
                cell1.PaddingBottom = 5f;
                subTable.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell();
                cell2.Border = Rectangle.NO_BORDER; // Quital el borde
                cell2.PaddingLeft = 9f;
                cell2.PaddingRight = 9f;
                cell2.PaddingTop = 5f;
                cell2.PaddingBottom = 5f;
                Chunk textChuckValorFabricante = new Chunk(Fabricante, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);
                Phrase phraseValorFabricante = new Phrase();
                phraseValorFabricante.Add(textChuckValorFabricante);
                phraseValorFabricante.Add(line);
                cell2.Phrase = new Phrase(phraseValorFabricante);
                subTable.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("Gravamen:", new Font(Font.FontFamily.HELVETICA, 9f)));
                cell3.Border = Rectangle.NO_BORDER; // Quital el borde
                cell3.PaddingLeft = 9f;
                cell3.PaddingRight = 9f;
                cell3.PaddingTop = 5f;
                cell3.PaddingBottom = 5f;
                subTable.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell();
                cell4.Border = Rectangle.NO_BORDER; // Quita el borde
                cell4.PaddingLeft = 9f;
                cell4.PaddingRight = 9f;
                cell4.PaddingTop = 5f;
                cell4.PaddingBottom = 5f;

                PdfPTable subTable4 = new PdfPTable(3);
                subTable4.DefaultCell.Border = Rectangle.NO_BORDER;
                subTable4.WidthPercentage = 100f; // O ajusta según sea necesario
                if (Gravamen == "Si")
                {
                    // Primer rectángulo con texto "CARGA"
                    PdfPCell rectangleCellA = new PdfPCell(new Phrase("SI", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellA.BackgroundColor = new BaseColor(192, 192, 192); // Fondo blanco
                    rectangleCellA.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellA.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCellA.VerticalAlignment = Element.ALIGN_CENTER;
                    subTable4.AddCell(rectangleCellA);

                }
                else
                {
                    // Primer rectángulo con texto "CARGA"
                    PdfPCell rectangleCellA = new PdfPCell(new Phrase("SI", new Font(Font.FontFamily.HELVETICA, 7f)));
                    rectangleCellA.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCellA.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellA.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCellA.VerticalAlignment = Element.ALIGN_CENTER;
                    subTable4.AddCell(rectangleCellA);
                }                
                // Segundo rectángulo con texto "PASAJEROS"
                PdfPCell rectangleCellB = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 7f)));
                rectangleCellB.BackgroundColor = BaseColor.WHITE;  // Fondo blanco
                rectangleCellB.Colspan = 1; // Ajusta según sea necesario
                rectangleCellB.Border = Rectangle.NO_BORDER; // Establece el borde a NO_BORDER para quitar los bordes
                subTable4.AddCell(rectangleCellB);

                if (Gravamen == "No")
                {
                    // Tercer rectángulo con texto "TRABAJO"
                    PdfPCell rectangleCellC = new PdfPCell(new Phrase("NO", new Font(Font.FontFamily.HELVETICA, 7f) { Color = BaseColor.BLACK }));
                    rectangleCellC.BackgroundColor = new BaseColor(192, 192, 192); // Fondo GRIS
                    rectangleCellC.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellC.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCellC.VerticalAlignment = Element.ALIGN_CENTER;
                    subTable4.AddCell(rectangleCellC);
                }
                else
                {
                    // Tercer rectángulo con texto "TRABAJO"
                    PdfPCell rectangleCellC = new PdfPCell(new Phrase("NO", new Font(Font.FontFamily.HELVETICA, 7f) { Color = BaseColor.BLACK }));
                    rectangleCellC.BackgroundColor = BaseColor.WHITE; // Fondo GRIS
                    rectangleCellC.Colspan = 1; // Ajusta según sea necesario
                    rectangleCellC.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCellC.VerticalAlignment = Element.ALIGN_CENTER;
                    subTable4.AddCell(rectangleCellC);
                }

                // Ajustar el ancho total de la tabla según sea necesario
                subTable4.TotalWidth = 200f; // Ajusta según sea necesario

                cell4.AddElement(subTable4);

                // Agregar la cuarta celda a la subtabla principal
                subTable.AddCell(cell4);

                // Agregar un salto de línea
                // Puedes ajustar el valor de SpacingAfter según tus necesidades para controlar el espacio después de la celda.
                subTable.SpacingAfter = 9f;

                return subTable;
            }
            static PdfPTable CreateCustomSubTable7(string observacionesGravamen)
            {
                PdfPTable subTable = new PdfPTable(2);
                float[] columnWidths = new float[] { 18f, 82f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);


                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;


                cell.Phrase = new Phrase("Descripción \ndel Gravamen:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);


                Chunk textChuck = new Chunk(observacionesGravamen, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phrase = new Phrase();
                phrase.Add(textChuck);
                phrase.Add(line);

                cell.Phrase = new Phrase(phrase);

                subTable.AddCell(cell);

                subTable.SpacingAfter = 9f;

                // Puedes agregar más celdas si es necesario

                return subTable;

            }
            /*************************************************************************FIN TABLA 1*************************************************************************/

            /*************************************************************************INICIO TABLA 2*************************************************************************/

            // Función para crear subtabla personalizada en la tabla 2 (Propio, Rentado)
            static PdfPTable CreateCustomSubTable2_1(string Regimen)
            {
                PdfPTable subTable = new PdfPTable(4);

                /*****COLUMNA TRACTIVO*****/
                PdfPCell cell1 = new PdfPCell();
                cell1.Border = Rectangle.NO_BORDER; // Quital el borde
                cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                PdfPTable innerTable = new PdfPTable(2);
                innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell textCell = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                textCell.Border = Rectangle.NO_BORDER; // Quital el borde
                innerTable.AddCell(textCell);
                // Agregar el rectángulo gris a la derecha
                PdfPCell rectangleCell = new PdfPCell();
                rectangleCell.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                rectangleCell.FixedHeight = 12f; // Ajustar la altura según sea necesario
                rectangleCell.Border = Rectangle.NO_BORDER; // Quitar el borde
                innerTable.AddCell(rectangleCell);
                cell1.AddElement(innerTable);
                subTable.AddCell(cell1);


                if (Regimen == "Propio")
                {
                    /*****COLUMNA ARRASTRE*****/
                    PdfPCell cell2 = new PdfPCell();
                    cell2.Border = Rectangle.NO_BORDER; // Quital el borde
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                                                                      // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                    PdfPTable innerTable2 = new PdfPTable(2);
                    innerTable2.DefaultCell.Border = Rectangle.NO_BORDER;
                    // Agregar el texto "Propio"
                    PdfPCell textCell2 = new PdfPCell(new Phrase("Propio", new Font(Font.FontFamily.HELVETICA, 9f)));
                    textCell2.Border = Rectangle.NO_BORDER; // Quital el borde
                    innerTable2.AddCell(textCell2);
                    // Agregar el rectángulo gris a la derecha                    
                    PdfPCell rectangleCell2 = new PdfPCell(new Phrase("X", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK }));
                    rectangleCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                    rectangleCell2.BackgroundColor = new BaseColor(192, 192, 192);  // Fondo blanco
                    rectangleCell2.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    innerTable2.AddCell(rectangleCell2);
                    cell2.AddElement(innerTable2);
                    subTable.AddCell(cell2);
                }
                else
                {
                    /*****COLUMNA ARRASTRE*****/
                    PdfPCell cell2 = new PdfPCell();
                    cell2.Border = Rectangle.NO_BORDER; // Quital el borde
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                                                                      // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                    PdfPTable innerTable2 = new PdfPTable(2);
                    innerTable2.DefaultCell.Border = Rectangle.NO_BORDER;
                    // Agregar el texto "Propio"
                    PdfPCell textCell2 = new PdfPCell(new Phrase("Propio", new Font(Font.FontFamily.HELVETICA, 9f)));
                    textCell2.Border = Rectangle.NO_BORDER; // Quital el borde
                    innerTable2.AddCell(textCell2);
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell2 = new PdfPCell();
                    rectangleCell2.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                    rectangleCell2.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    innerTable2.AddCell(rectangleCell2);
                    cell2.AddElement(innerTable2);
                    subTable.AddCell(cell2);
                }


                if (Regimen == "Arrendado")
                {
                    /*****COLUMNA TRABAJO*****/
                    PdfPCell cell3 = new PdfPCell();
                    cell3.Border = Rectangle.NO_BORDER; // Quital el borde
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                                                                      // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                    PdfPTable innerTable3 = new PdfPTable(2);
                    innerTable3.DefaultCell.Border = Rectangle.NO_BORDER;
                    // Agregar el texto "Rentado"
                    PdfPCell textCell3 = new PdfPCell(new Phrase("Rentado", new Font(Font.FontFamily.HELVETICA, 9f)));
                    textCell3.Border = Rectangle.NO_BORDER; // Quital el borde
                    innerTable3.AddCell(textCell3);
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell3 = new PdfPCell(new Phrase("X", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK }));
                    rectangleCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    rectangleCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                    rectangleCell3.BackgroundColor = new BaseColor(192, 192, 192);  // Color gris
                    rectangleCell3.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    rectangleCell3.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCell3.VerticalAlignment = Element.ALIGN_CENTER;
                    innerTable3.AddCell(rectangleCell3);
                    cell3.AddElement(innerTable3);
                    subTable.AddCell(cell3);
                }
                else
                {
                    /*****COLUMNA TRABAJO*****/
                    PdfPCell cell3 = new PdfPCell();
                    cell3.Border = Rectangle.NO_BORDER; // Quital el borde
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                                                                      // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                    PdfPTable innerTable3 = new PdfPTable(2);
                    innerTable3.DefaultCell.Border = Rectangle.NO_BORDER;
                    // Agregar el texto "Rentado"
                    PdfPCell textCell3 = new PdfPCell(new Phrase("Rentado", new Font(Font.FontFamily.HELVETICA, 9f)));
                    textCell3.Border = Rectangle.NO_BORDER; // Quital el borde
                    innerTable3.AddCell(textCell3);
                    // Agregar el rectángulo gris a la derecha
                    PdfPCell rectangleCell3 = new PdfPCell();
                    rectangleCell3.BackgroundColor = BaseColor.WHITE;  // Color gris
                    rectangleCell3.FixedHeight = 12f; // Ajustar la altura según sea necesario
                    rectangleCell3.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el texto
                    rectangleCell3.VerticalAlignment = Element.ALIGN_CENTER;
                    innerTable3.AddCell(rectangleCell3);
                    cell3.AddElement(innerTable3);
                    subTable.AddCell(cell3);
                }

                PdfPCell cell4 = new PdfPCell();
                cell4.Border = Rectangle.NO_BORDER; // Quitar el borde
                cell4.HorizontalAlignment = Element.ALIGN_CENTER; // Centrar el contenido
                // Crear una tabla interna para agregar el texto y el rectángulo gris a la derecha
                PdfPTable innerTable4 = new PdfPTable(2);
                innerTable4.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell textCell4 = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                textCell4.Border = Rectangle.NO_BORDER; // Quitar el borde
                innerTable4.AddCell(textCell4);
                // Agregar el rectángulo gris a la derecha
                PdfPCell rectangleCell4 = new PdfPCell();
                rectangleCell4.BackgroundColor = BaseColor.WHITE; // Fondo blanco
                rectangleCell4.FixedHeight = 12f; // Ajustar la altura según sea necesario
                rectangleCell4.Border = Rectangle.NO_BORDER; // Quitar el borde
                innerTable4.AddCell(rectangleCell4);
                cell4.AddElement(innerTable4);
                subTable.AddCell(cell4);


                return subTable;
            }
            // Función para crear subtabla personalizada en la tabla 2 (Contrato y fecha de contrato)
            static PdfPTable CreateCustomSubTable2_2(string TipoContrato, string FechaContrato)
            {
                FechaContrato = FechaContrato.Replace("12:00:00 a. m.", "");
                FechaContrato = FechaContrato.Replace("00:00:00", "");

                PdfPTable subTable = new PdfPTable(4);
                float[] columnWidths = new float[] { 18f, 32f, 18f, 32f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);



                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;


                cell.Phrase = new Phrase("Contrato", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);


                Chunk textChuckValorContrato = new Chunk(TipoContrato, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phraseValorContrato = new Phrase();
                phraseValorContrato.Add(textChuckValorContrato);
                phraseValorContrato.Add(line);
                cell.Phrase = new Phrase(phraseValorContrato);
                subTable.AddCell(cell);


                cell.Phrase = new Phrase("Fecha Contrato:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);
                // Puedes agregar más celdas si es necesario

                Chunk textChuckValorFecha = new Chunk(FechaContrato, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                Phrase phraseValorFecha = new Phrase();
                phraseValorFecha.Add(textChuckValorFecha);
                phraseValorFecha.Add(line);
                cell.Phrase = new Phrase(phraseValorFecha);
                subTable.AddCell(cell);

                return subTable;
            }

            // Función para crear subtabla personalizada en la tabla 2 (Nombre del propietario)
            static PdfPTable CreateCustomSubTable2_3(string nombrePropietario)
            {
                PdfPTable subTable = new PdfPTable(2);
                float[] columnWidths = new float[] { 18f, 82f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);


                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;


                cell.Phrase = new Phrase("Nombre del Propietario:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);


                Chunk textChuck = new Chunk(nombrePropietario, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phrase = new Phrase();
                phrase.Add(textChuck);
                phrase.Add(line);

                cell.Phrase = new Phrase(phrase);

                subTable.AddCell(cell);


                // Puedes agregar más celdas si es necesario

                return subTable;


            }

            // Función para crear subtabla personalizada en la tabla 2 (Nombre de usuario)

            static PdfPTable CreateCustomSubTable2_4(string nombreUsuario)
            {
                PdfPTable subTable = new PdfPTable(2);
                float[] columnWidths = new float[] { 18f, 82f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new Phrase("Nombre del Usuario:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);

                Chunk textChuck = new Chunk(nombreUsuario, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phrase = new Phrase();
                phrase.Add(textChuck);
                phrase.Add(line);

                cell.Phrase = new Phrase(phrase);

                subTable.AddCell(cell);

                return subTable;

            }
            // Función para crear subtabla personalizada en la tabla 2 (Descripcion)
            static PdfPTable CreateCustomSubTable2_5(string Descripcion_Equipo)
            {
                PdfPTable subTable = new PdfPTable(2);
                float[] columnWidths = new float[] { 18f, 82f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                cell.Phrase = new Phrase("Descripción:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);

                Chunk textChuck = new Chunk(Descripcion_Equipo, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phrase = new Phrase();
                phrase.Add(textChuck);
                phrase.Add(line);

                cell.Phrase = new Phrase(phrase);
                subTable.AddCell(cell);

                // Puedes agregar más celdas si es necesario

                return subTable;


            }
            // Función para crear subtabla personalizada en la tabla 2 (Observaciones)
            static PdfPTable CreateCustomSubTable2_6(string ObservacionesInscripcion)
            {
                PdfPTable subTable = new PdfPTable(2);
                float[] columnWidths = new float[] { 18f, 82f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                cell.Phrase = new Phrase("Observaciones:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);

                Chunk textChuck = new Chunk(ObservacionesInscripcion, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phrase = new Phrase();
                phrase.Add(textChuck);
                phrase.Add(line);

                cell.Phrase = new Phrase(phrase);
                subTable.AddCell(cell);

                // Puedes agregar más celdas si es necesario

                return subTable;

            }
            // Función para crear subtabla personalizada en la tabla 2 (No de oficio y Fecha oficio)
            static PdfPTable CreateCustomSubTable2_7(string noOficio, string Fecha)
            {

                Fecha = Fecha.Replace("12:00:00 a. m.", "");
                Fecha = Fecha.Replace("00:00:00", "");


                PdfPTable subTable = new PdfPTable(4);
                float[] columnWidths = new float[] { 18f, 32f, 18f, 32f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new Phrase("No. de Oficio:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);

                Chunk textChuckValorOficio = new Chunk(noOficio, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                LineSeparator line = new LineSeparator(0.5f, 100, BaseColor.BLACK, Element.ALIGN_LEFT, -2);

                Phrase phraseValorOficio = new Phrase();
                phraseValorOficio.Add(textChuckValorOficio);
                phraseValorOficio.Add(line);
                cell.Phrase = new Phrase(phraseValorOficio);
                subTable.AddCell(cell);

                cell.Phrase = new Phrase("Fecha de Oficio:", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);
                // Puedes agregar más celdas si es necesario

                Chunk textChuckValorFecha = new Chunk(Fecha, new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                Phrase phraseValorFecha = new Phrase();
                phraseValorFecha.Add(textChuckValorFecha);
                phraseValorFecha.Add(line);
                cell.Phrase = new Phrase(phraseValorFecha);
                subTable.AddCell(cell);
                // Agregar un salto de línea
                // Puedes ajustar el valor de SpacingAfter según tus necesidades para controlar el espacio después de la celda.
                subTable.SpacingAfter = 9f;
                return subTable;

            }
            /*************************************************************************FIN TABLA 2*************************************************************************/
            /*************************************************************************INICIO TABLA 3*************************************************************************/
            /*************************************************************************FIN TABLA 3*************************************************************************/
            static PdfPTable CreateCustomSubTable3_1(string fechaEquipo)
            {
                fechaEquipo = fechaEquipo.Replace("12:00:00 a. m.", "");
                fechaEquipo = fechaEquipo.Replace("00:00:00", "");

                // Convierte la cadena a un objeto DateTime
                DateTime fecha = DateTime.ParseExact(fechaEquipo.Trim(), "dd/MM/yyyy", null);

                // Formatea la fecha según tus preferencias
                string fechaFormateada = fecha.ToString("dd 'de' MMMM 'de' yyyy");


                PdfPTable subTable = new PdfPTable(1);
                float[] columnWidths = new float[] { 100f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;                
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                cell.Phrase = new Phrase($"Con folio No. RFMETCOIN543 se hace constar que con fecha {fechaFormateada} queda inscrito el Equipo descrito en el presente documento y sus especificaciones técnicas mostradas al reverso en el Registro Ferroviario Mexicano de conformidad a lo estipulado en el artículo 204, fracción III del Reglamento del Servicio Ferroviario.", new Font(Font.FontFamily.HELVETICA, 9f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);

                return subTable;
            }

            static PdfPTable CreateCustomSubTable3_2(string fechaEquipo)
            {
                fechaEquipo = fechaEquipo.Replace("12:00:00 a. m.", "");
                fechaEquipo = fechaEquipo.Replace("00:00:00", "");

                PdfPTable subTable = new PdfPTable(1);
                float[] columnWidths = new float[] { 100f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                string generatedString = GenerateSelloDigital(144);
                string sello = GenerateCadenaSello(13);
                string constanciaSituacionFiscal = GenerateConstanciaSituacionFiscal(21);

                cell.Phrase = new Phrase($"Cadena Original Sello: ||{fechaEquipo}|{sello}|CONSTANCIA DE SITUACIÓN FISCAL|{constanciaSituacionFiscal}||\nSello Digital: {generatedString}", new Font(Font.FontFamily.HELVETICA, 8f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);

                // Puedes agregar más celdas si es necesario

                return subTable;

            }
            static PdfPTable CreateCustomSubTable3_3()
            {
                PdfPTable subTable = new PdfPTable(1);
                float[] columnWidths = new float[] { 100f }; // Porcentajes de ancho para cada columna

                // Establecer los anchos de las columnas
                subTable.SetWidths(columnWidths);

                PdfPCell cell = new PdfPCell();

                cell.Border = Rectangle.NO_BORDER; // Quita el borde
                cell.PaddingLeft = 9f;
                cell.PaddingRight = 9f;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;

                cell.Phrase = new Phrase("RFM_DGEERFM", new Font(Font.FontFamily.HELVETICA, 8f) { Color = BaseColor.BLACK });
                subTable.AddCell(cell);
                subTable.SpacingAfter = 5f;
                // Puedes agregar más celdas si es necesario

                return subTable;

            }
            static string GenerateSelloDigital(int length)
            {
                // Caracteres que puedes incluir en tu string
                string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/+.";

                // Genera un string aleatorio de la misma longitud
                StringBuilder result = new StringBuilder();
                Random random = new Random();

                for (int i = 0; i < length; i++)
                {
                    result.Append(characters[random.Next(characters.Length)]);
                }

                return result.ToString();
            }
            static string GenerateCadenaSello(int length)
            {
                // Caracteres que puedes incluir en tu string
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/+.";

                // Genera un string aleatorio de la misma longitud
                StringBuilder result = new StringBuilder();
                Random random = new Random();

                for (int i = 0; i < length; i++)
                {
                    result.Append(characters[random.Next(characters.Length)]);
                }

                return result.ToString();
            }
            static string GenerateConstanciaSituacionFiscal(int length)
            {
                // Caracteres que puedes incluir en tu string
                string characters = "0123456789/+.";

                // Genera un string aleatorio de la misma longitud
                StringBuilder result = new StringBuilder();
                Random random = new Random();

                for (int i = 0; i < length; i++)
                {
                    result.Append(characters[random.Next(characters.Length)]);
                }

                return result.ToString();
            }
        }
    }
}
