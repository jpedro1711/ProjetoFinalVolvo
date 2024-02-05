using System.Diagnostics;
using System.IO;
using ConcessionariaAPI.Repositories.Dto;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ConcessionariaAPI.Relatorio{
    public class RelatorioPDF{

        /*private List<BalancoFinanceiro> balancoFinanceiro;

        public Relatorio(List<BalancoFinanceiro> balancoFinanceiro){
            this.balancoFinanceiro = balancoFinanceiro;
        }*/

        static BaseFont fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

        public static void gerarRelatorioPDF(List<BalancoFinanceiro> balancoFinanceiro)
        {
            //Cálculo da quantidade total de páginas
            int totalPaginas = 1;
            int totalLinhas = balancoFinanceiro.Count;
            if(totalLinhas > 24)
                totalPaginas += (int)Math.Ceiling((totalLinhas - 24) / 29F);           

            //Configuração do PDF
            var pxPorMm = 72 / 25.2f;
            var pdf = new Document(PageSize.A4, 15 * pxPorMm, 15 * pxPorMm,
                                    15 * pxPorMm, 20 * pxPorMm);

            var nomeArquivo = $"balancoFinanceiro.{DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss")}.pdf";
            var arquivo = new FileStream(nomeArquivo, FileMode.Create);
            var writer = PdfWriter.GetInstance(pdf, arquivo);
            writer.PageEvent = new EventosDePagina(totalPaginas);
            pdf.Open();            

            //Adição do Título
            var fonteParagrafo = new iTextSharp.text.Font(fonteBase, 32,
                                    iTextSharp.text.Font.NORMAL, BaseColor.Black);
            var titulo = new Paragraph("Relatório de Balanço Financeiro\n\n", fonteParagrafo);
            titulo.Alignment = Element.ALIGN_LEFT;
            titulo.SpacingAfter = 4;
            pdf.Add(titulo);

            //Adição da Logo
            /*string caminhoImagem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Relatorio\\img\\Logo.jpg");
            if (File.Exists(caminhoImagem))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(caminhoImagem);
                float razaoAlturaLargura = logo.Width / logo.Height;
                float alturaLogo = 32;
                float larguraLogo = alturaLogo * razaoAlturaLargura;
                logo.ScaleToFit(larguraLogo, alturaLogo);
                var margemEsquerda = pdf.PageSize.Height - pdf.TopMargin - 54;
                var margemTopo = pdf.PageSize.Height - pdf.TopMargin - 54;
                logo.SetAbsolutePosition(margemEsquerda, margemTopo);
                writer.DirectContent.AddImage(logo, false);
            }*/

            //Adição da tabela de dados
            var tabela = new PdfPTable(5);
            float[] largurasColunas = {0.5f, 0.5f, 1f, 1f, 1f};
            tabela.SetWidths(largurasColunas);
            tabela.DefaultCell.BorderWidth = 0;
            tabela.WidthPercentage = 100;

            //Adição das células de títulos das colunas            
            CriarCelulaTexto(tabela, "Mês", PdfCell.ALIGN_CENTER, true);            
            CriarCelulaTexto(tabela, "Ano", PdfCell.ALIGN_CENTER, true);
            CriarCelulaTexto(tabela, "Custos", PdfCell.ALIGN_CENTER, true);
            CriarCelulaTexto(tabela, "Vendido", PdfCell.ALIGN_CENTER, true);
            CriarCelulaTexto(tabela, "%Lucro", PdfCell.ALIGN_CENTER, true);

            foreach(BalancoFinanceiro bf in balancoFinanceiro){
                CriarCelulaTexto(tabela, bf.Mes.ToString("D2"), PdfPCell.ALIGN_CENTER);
                CriarCelulaTexto(tabela, bf.Ano.ToString("D4"), PdfPCell.ALIGN_CENTER);
                CriarCelulaTexto(tabela, bf.Custos.ToString("C"), PdfPCell.ALIGN_CENTER);
                CriarCelulaTexto(tabela, bf.Vendido.ToString("C"), PdfPCell.ALIGN_CENTER);
                CriarCelulaTexto(tabela, bf.PercLucro.ToString("F0") + "%", PdfPCell.ALIGN_CENTER);
            }

            pdf.Add(tabela);

            pdf.Close();
            arquivo.Close();

            //Abre o PDF no visualizador padrão
            var caminhoPDF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
            if (File.Exists(caminhoPDF))
            {
                Process.Start(new ProcessStartInfo()
                {
                    Arguments = $"/c start {caminhoPDF}",
                    FileName = "cmd.exe",
                    CreateNoWindow = true
                });
            }

        }

        private static void CriarCelulaTexto(PdfPTable tabela, string texto,
            int alinhamentoHorz = PdfPCell.ALIGN_LEFT,
            bool negrito = false, bool italico = false,
            int tamanhoFonte = 12, int alturaCelula = 25)
        {
            int estilo = iTextSharp.text.Font.NORMAL;
            if(negrito && italico){
                estilo = iTextSharp.text.Font.BOLDITALIC;
            }else if(negrito){
                estilo = iTextSharp.text.Font.BOLD;
            }else if(italico){
                estilo = iTextSharp.text.Font.ITALIC;
            }
            var fonteCelula = new iTextSharp.text.Font(fonteBase, tamanhoFonte,
                               estilo, BaseColor.Black);

            var bgColor = iTextSharp.text.BaseColor.White;
            if(tabela.Rows.Count % 2 == 1)
                bgColor = new BaseColor(0.95f, 0.95f, 0.95f);

            var celula = new PdfPCell(new Phrase(texto, fonteCelula));
            celula.HorizontalAlignment = alinhamentoHorz;
            celula.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
            celula.Border = 0;
            celula.BorderWidthBottom = 1;
            celula.FixedHeight = alturaCelula;
            celula.PaddingBottom = 5;
            celula.BackgroundColor = bgColor;
            tabela.AddCell(celula);
        }
    }
}