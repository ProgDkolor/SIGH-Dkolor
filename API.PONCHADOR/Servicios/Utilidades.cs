using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.PONCHADOR.Modelo;
using System.Data;
using Infragistics.Documents.Excel;
using System.IO;
using System.Drawing;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace API.PONCHADOR.Servicios
{
    public class Utilidades
    {
        public static List<Parametros> ToParametros(List<ParametrosJson> d)
        {
            List<Parametros> res = new List<Parametros>();
            d.ForEach(e => {
                Parametros p = new Parametros()
                { 
                   Tipo=(DbType)e.Tipo,
                   Valor=e.Valor,
                   Parametro=e.Parametro
                };
                res.Add(p);
                
            });
            return res;
        }

        public static byte[] toByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] GeneraArchivoBanco(List<DetallePagosNomina> data)
        {
            byte[] res = null;
            StringBuilder archivo = new StringBuilder();
            data.OrderBy(e=>e.CuentaBancaria).ToList().ForEach(d => {
                archivo.AppendLine($"CC,DOP,{d.CuentaEmpresa},CA,DOP,{d.CuentaBancaria},{d.Valor},{d.DescripcionTipoPago}");
            });
            using (var ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms))
                {
                    tw.Write(archivo.ToString());
                    tw.Flush();
                    ms.Position = 0;
                    res = ms.ToArray();
                }
            }
            return res;


        }
            public static MemoryStream GeneraExcelNomina(List<ReporteNomina> data, string nombre,string descripcionNomina)
        {
            Workbook libro = new Workbook(WorkbookFormat.Excel97To2003);
            Worksheet hoja = libro.Worksheets.Add(nombre);

            int filaInicio = 2;
            hoja.Rows[filaInicio].Cells[0].Value = "MODULO";
            hoja.Rows[filaInicio].Cells[1].Value = "NOMBRE";
            hoja.Rows[filaInicio].Cells[2].Value = "CEDULA";
            hoja.Rows[filaInicio].Cells[3].Value = "INGRESO";
            hoja.Rows[filaInicio].Cells[4].Value = "SUELDO";
            hoja.Rows[filaInicio].Cells[5].Value = "TOTAL";
            hoja.Rows[filaInicio].Cells[6].Value = "NOMINA";
            hoja.Rows[filaInicio].Cells[7].Value = "EFECTIVO";
            hoja.Rows[filaInicio].Cells[8].Value = "ISR";
            hoja.Rows[filaInicio].Cells[9].Value = "DESC";
            hoja.Rows[filaInicio].Cells[10].Value = "TSS-SFS";
            hoja.Rows[filaInicio].Cells[11].Value = "NETO NOMINA";
            hoja.Rows[filaInicio].Cells[12].Value = "TOTAL A PAGAR";
            hoja.Rows[filaInicio].Cells[13].Value = "# CUENTA";
            //borde izquierdo cabecera
            hoja.Rows[filaInicio].Cells[0].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[1].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[2].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[3].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[4].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[5].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[6].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[7].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[8].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[9].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[10].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[11].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[12].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[13].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[14].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            //borde abajo cabecera
            hoja.Rows[filaInicio].Cells[0].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[1].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[2].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[3].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[4].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[5].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[6].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[7].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[8].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[9].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[10].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[11].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[12].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[13].CellFormat.BottomBorderStyle = CellBorderLineStyle.Thin;
            //borde arriba cabecera
            hoja.Rows[filaInicio].Cells[0].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[1].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[2].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[3].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[4].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[5].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[6].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[7].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[8].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[9].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[10].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[11].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[12].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaInicio].Cells[13].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;

            hoja.Columns[0].Width = 10000;
            hoja.Columns[1].Width = 16000;
            hoja.Columns[2].Width = 6000;
            hoja.Columns[3].Width = 6000;
            hoja.Columns[4].Width = 6000;
            hoja.Columns[5].Width = 6000;
            hoja.Columns[6].Width = 6000;
            hoja.Columns[7].Width = 6000;
            hoja.Columns[8].Width = 6000;
            hoja.Columns[9].Width = 6000;
            hoja.Columns[10].Width =6000;
            hoja.Columns[11].Width = 6000;
            hoja.Columns[12].Width = 6000;
            hoja.Columns[13].Width = 6000;
            hoja.Columns[14].Width = 6000;
            hoja.Columns[0].CellFormat.Alignment = HorizontalCellAlignment.Center;
            hoja.Columns[4].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[5].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[6].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[7].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[8].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[9].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[10].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[11].CellFormat.Alignment = HorizontalCellAlignment.Right;
            hoja.Columns[12].CellFormat.Alignment = HorizontalCellAlignment.Right;

            //hoja.Columns[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[0].CellFormat.Alignment = HorizontalCellAlignment.Center;
            hoja.Rows[2].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Columns[4].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[5].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[6].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[7].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[8].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[9].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[10].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[11].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Columns[12].CellFormat.FormatString = "\"\"#,##0.00_);[Red](\"\"#,##0.00)";
            hoja.Rows[0].Height = 800;
            hoja.Rows[2].Height = 800;
            hoja.Rows[2].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            hoja.Rows[2].CellFormat.Font.Height = 18 * 20;
            hoja.Rows[2].Height = 800;
            hoja.Rows[2].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            //hoja.Columns[5].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
            ////string ruta = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data");
            //////libro.Save($"{ruta}/Reportes/{"REPORTE V1"}.xls");
            ////Image imgPagoRd = Image.FromFile($"{ruta}/img/pagoRd.png");
            ////WorksheetImage logo = new WorksheetImage(imgPagoRd);
            ////WorksheetMergedCellsRegion celdaLogo = hoja.MergedCellsRegions.Add(0, 0, 1, 3);
            ////WorksheetCell cellA1 = hoja.Rows[0].Cells[2];

            ////WorksheetCell cellA2 = hoja.Rows[0].Cells[0];

            // The top-left corner of the image should be at the
            // top-left corner of cell A1
            //logo.TopLeftCornerCell = cellA1;
            //logo.TopLeftCornerPosition = new PointF(0.0F, 0.0F);
            // The bottom-right corner of the image should be at
            // the bottom-right corner of cell A1
            //logo.BottomRightCornerCell = cellA1;
            //logo.BottomRightCornerPosition = new PointF(500.0F, 300.0F);
            //hoja.Shapes.Add(logo);

            //libro.Worksheets[0].Workbook.fi.FilterSettings.SetRegion("A2:G2");

            WorksheetMergedCellsRegion tituloNomina = hoja.MergedCellsRegions.Add(0, 0, 1, 13);
            tituloNomina.Value = descripcionNomina;
            tituloNomina.CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
            WorkbookColorInfo colorAzul = new WorkbookColorInfo(Color.Blue);
            tituloNomina.CellFormat.Font.ColorInfo = colorAzul;
            tituloNomina.CellFormat.Font.Name = "arial";
            tituloNomina.CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            tituloNomina.CellFormat.Font.Height = 18 * 20;

            //          Formula orderTotalFormula =Formula.Parse("=C2*D2* (1-E2)",CellReferenceMode.A1);

            //          Infragistics.Documents.Excel.WorksheetRegion region =
            //new Infragistics.Documents.Excel.WorksheetRegion(hoja, 1, 5, 4, 5);



            //orderTotalFormula.ApplyTo(region);

            //mergedCell.CellFormat.Alignment =HorizontalCellAlignment.Right;


            int filaDatos = 3;
            List<int> modulos = data.Select(d => d.IdModulo).Distinct().ToList();

            
            modulos.ForEach(m => {
                int inicioSuma = filaDatos + 1;
                int finSuma = filaDatos;
                foreach (ReporteNomina r in data.FindAll(d=>d.IdModulo==m).OrderBy(e=>e.Nombre))
                {
                    finSuma++;
                    WorksheetRow row = hoja.Rows[filaDatos++];
                    row.Height = 800;
                    row.CellFormat.Font.Height = 16 * 20;
                    row.CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                    row.Cells[0].Value = r.Modulo;
                    row.Cells[1].Value = r.Nombre;
                    row.Cells[2].Value = r.Cedula;
                    row.Cells[3].Value = string.Format("{0:dd-MM-yyyy}", r.FechaIngreso).ToUpper();
                    row.Cells[4].Value = r.Sueldo;
                    row.Cells[5].Value = r.SueldoQuincenal;
                    row.Cells[6].Value = r.SueldoQuincenal;
                    row.Cells[7].Value = r.PagoEfectivo;
                    row.Cells[8].Value = r.ISR;
                    row.Cells[9].Value = r.Descuento;
                    row.Cells[10].Value = r.AFP + r.TSS_SFS;
                    row.Cells[11].Value = r.SueldoNeto;
                    row.Cells[12].Value = r.SueldoNeto;
                    row.Cells[13].Value = r.CuentaBanco;

                    row.Cells[0].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[1].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[2].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[3].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[4].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[5].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[6].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[7].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[8].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[9].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[10].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[11].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[12].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[13].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                    row.Cells[14].CellFormat.LeftBorderStyle = CellBorderLineStyle.Thin;
                }
                hoja.Rows[filaDatos].Cells[0].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[1].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[2].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[3].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[4].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[5].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[6].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[7].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[8].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[9].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[10].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[11].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[12].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[13].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;

                hoja.Rows[filaDatos].CellFormat.Font.Height = 18 * 20;

                WorkbookColorInfo colorRojo = new WorkbookColorInfo(Color.Red);
                hoja.Rows[filaDatos].Cells[2].Value = $"Sub-Total {data.Find(d=>d.IdModulo==m).Modulo}";
                hoja.Rows[filaDatos].Cells[2].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[2].CellFormat.Font.ColorInfo = colorRojo;


                Formula totalValor4 = Formula.Parse($"=SUM($E${inicioSuma}:$E${finSuma})", CellReferenceMode.A1);
                totalValor4.ApplyTo(hoja.Rows[filaDatos].Cells[4]);
                hoja.Rows[filaDatos].Cells[4].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[4].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor5 = Formula.Parse($"=SUM($F${inicioSuma}:$F${finSuma})", CellReferenceMode.A1);
                totalValor5.ApplyTo(hoja.Rows[filaDatos].Cells[5]);
                hoja.Rows[filaDatos].Cells[5].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[5].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor6 = Formula.Parse($"=SUM($G${inicioSuma}:$G${finSuma})", CellReferenceMode.A1);
                totalValor6.ApplyTo(hoja.Rows[filaDatos].Cells[6]);
                hoja.Rows[filaDatos].Cells[6].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[6].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor7 = Formula.Parse($"=SUM($H${inicioSuma}:$H${finSuma})", CellReferenceMode.A1);
                totalValor7.ApplyTo(hoja.Rows[filaDatos].Cells[7]);
                hoja.Rows[filaDatos].Cells[7].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[7].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor8 = Formula.Parse($"=SUM($I${inicioSuma}:$I${finSuma})", CellReferenceMode.A1);
                totalValor8.ApplyTo(hoja.Rows[filaDatos].Cells[8]);
                hoja.Rows[filaDatos].Cells[8].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[8].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor9 = Formula.Parse($"=SUM($J${inicioSuma}:$J${finSuma})", CellReferenceMode.A1);
                totalValor9.ApplyTo(hoja.Rows[filaDatos].Cells[9]);
                hoja.Rows[filaDatos].Cells[9].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[9].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor10 = Formula.Parse($"=SUM($K${inicioSuma}:$K${finSuma})", CellReferenceMode.A1);
                totalValor10.ApplyTo(hoja.Rows[filaDatos].Cells[10]);
                hoja.Rows[filaDatos].Cells[10].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[10].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor11 = Formula.Parse($"=SUM($L${inicioSuma}:$L${finSuma})", CellReferenceMode.A1);
                totalValor11.ApplyTo(hoja.Rows[filaDatos].Cells[11]);
                hoja.Rows[filaDatos].Cells[11].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[11].CellFormat.Font.ColorInfo = colorRojo;

                Formula totalValor12 = Formula.Parse($"=SUM($M${inicioSuma}:$M${finSuma})", CellReferenceMode.A1);
                totalValor12.ApplyTo(hoja.Rows[filaDatos].Cells[12]);
                hoja.Rows[filaDatos].Cells[12].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                hoja.Rows[filaDatos].Cells[12].CellFormat.Font.ColorInfo = colorRojo;

                filaDatos++;
                hoja.Rows[filaDatos].Height = 500;
                filaDatos++;
                filaDatos++;

                hoja.Rows[filaDatos].Cells[0].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[1].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[2].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[3].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[4].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[5].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[6].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[7].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[8].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[9].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[10].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[11].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[12].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
                hoja.Rows[filaDatos].Cells[13].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;


            });


            filaDatos++;
            hoja.Rows[filaDatos].Cells[0].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[1].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[2].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[3].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[4].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[5].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[6].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[7].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[8].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[9].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[10].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[11].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[12].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[13].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            filaDatos++;
            hoja.Rows[filaDatos].Height = 500;
            hoja.Rows[filaDatos].CellFormat.Font.Height = 20 * 20;

            WorkbookColorInfo colorRojo = new WorkbookColorInfo(Color.Red);
            hoja.Rows[filaDatos].Cells[2].Value = $"GRAN TOTAL";
            hoja.Rows[filaDatos].Cells[2].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[2].CellFormat.Font.ColorInfo = colorRojo;


            //Formula totalValor4 = Formula.Parse($"=SUM($E${inicioSuma}:$E${finSuma})", CellReferenceMode.A1);
            //totalValor4.ApplyTo(hoja.Rows[filaDatos].Cells[4]);
            hoja.Rows[filaDatos].Cells[4].Value = data.Sum(d => d.Sueldo);
            hoja.Rows[filaDatos].Cells[4].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[4].CellFormat.Font.ColorInfo = colorRojo;

            //Formula totalValor5 = Formula.Parse($"=SUM($F${inicioSuma}:$F${finSuma})", CellReferenceMode.A1);
            //totalValor5.ApplyTo(hoja.Rows[filaDatos].Cells[5]);
            hoja.Rows[filaDatos].Cells[5].Value = data.Sum(d => d.SueldoQuincenal);
            hoja.Rows[filaDatos].Cells[5].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[5].CellFormat.Font.ColorInfo = colorRojo;

            ////Formula totalValor6 = Formula.Parse($"=SUM($G${inicioSuma}:$G${finSuma})", CellReferenceMode.A1);
            ////totalValor6.ApplyTo(hoja.Rows[filaDatos].Cells[6]);
            hoja.Rows[filaDatos].Cells[6].Value = data.Sum(d => d.SueldoQuincenal);
            hoja.Rows[filaDatos].Cells[6].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[6].CellFormat.Font.ColorInfo = colorRojo;

            //Formula totalValor7 = Formula.Parse($"=SUM($H${inicioSuma}:$H${finSuma})", CellReferenceMode.A1);
            //totalValor7.ApplyTo(hoja.Rows[filaDatos].Cells[7]);
            hoja.Rows[filaDatos].Cells[7].Value = data.Sum(d => d.PagoEfectivo);
            hoja.Rows[filaDatos].Cells[7].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[7].CellFormat.Font.ColorInfo = colorRojo;

            ////Formula totalValor8 = Formula.Parse($"=SUM($I${inicioSuma}:$I${finSuma})", CellReferenceMode.A1);
            ////totalValor8.ApplyTo(hoja.Rows[filaDatos].Cells[8]);
            hoja.Rows[filaDatos].Cells[8].Value = data.Sum(d => d.ISR);
            hoja.Rows[filaDatos].Cells[8].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[8].CellFormat.Font.ColorInfo = colorRojo;

            //Formula totalValor9 = Formula.Parse($"=SUM($J${inicioSuma}:$J${finSuma})", CellReferenceMode.A1);
            //totalValor9.ApplyTo(hoja.Rows[filaDatos].Cells[9]);
            hoja.Rows[filaDatos].Cells[9].Value = data.Sum(d => d.Descuento);
            hoja.Rows[filaDatos].Cells[9].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[9].CellFormat.Font.ColorInfo = colorRojo;

            //Formula totalValor10 = Formula.Parse($"=SUM($K${inicioSuma}:$K${finSuma})", CellReferenceMode.A1);
            //totalValor10.ApplyTo(hoja.Rows[filaDatos].Cells[10]);
            hoja.Rows[filaDatos].Cells[10].Value = data.Sum(d => d.AFP+d.TSS_SFS);
            hoja.Rows[filaDatos].Cells[10].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[10].CellFormat.Font.ColorInfo = colorRojo;

            //Formula totalValor11 = Formula.Parse($"=SUM($L${inicioSuma}:$L${finSuma})", CellReferenceMode.A1);
            //totalValor11.ApplyTo(hoja.Rows[filaDatos].Cells[11]);
            hoja.Rows[filaDatos].Cells[11].Value = data.Sum(d => d.SueldoNeto);
            hoja.Rows[filaDatos].Cells[11].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[11].CellFormat.Font.ColorInfo = colorRojo;

            //Formula totalValor12 = Formula.Parse($"=SUM($M${inicioSuma}:$M${finSuma})", CellReferenceMode.A1);
            //totalValor12.ApplyTo(hoja.Rows[filaDatos].Cells[12]);
            hoja.Rows[filaDatos].Cells[12].Value = data.Sum(d => d.SueldoNeto);
            hoja.Rows[filaDatos].Cells[12].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
            hoja.Rows[filaDatos].Cells[12].CellFormat.Font.ColorInfo = colorRojo;

            hoja.Rows[filaDatos].Cells[0].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[1].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[2].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[3].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[4].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[5].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[6].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[7].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[8].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[9].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[10].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[11].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[12].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            hoja.Rows[filaDatos].Cells[13].CellFormat.TopBorderStyle = CellBorderLineStyle.Thin;
            MemoryStream m = new MemoryStream();
            libro.Save(m);
            return (m);
        }
    }
}
