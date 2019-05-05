/********************************************************************************
 *   This file is part of NRtfTree Library.
 *
 *   NRtfTree Library is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU Lesser General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   NRtfTree Library is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU Lesser General Public License for more details.
 *
 *   You should have received a copy of the GNU Lesser General Public License
 *   along with this program. If not, see <http://www.gnu.org/licenses/>.
 ********************************************************************************/

/********************************************************************************
 * Library:		NRtfTree
 * Version:     v0.3.0
 * Date:		02/09/2007
 * Copyright:   2007 Salvador Gomez
 * E-mail:      sgoliver.net@gmail.com
 * Home Page:	http://www.sgoliver.net
 * SF Project:	http://nrtftree.sourceforge.net
 *				http://sourceforge.net/projects/nrtftree
 * Class:		Pruebas
 * Description:	Programa principal de demostración de la librería.
 ********************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Net.Sgoliver.NRtfTree.Core;
using Net.Sgoliver.NRtfTree.Util;
using System.Xml;
using System.Drawing.Imaging;

namespace Pruebas
{
	public class Prueba : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtArbol;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.RichTextBox rtxtTexto;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private Button button5;
        private Label label3;
        private Button button6;
        private Button button7;
		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Punto de entrada principal de la aplicación.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Prueba());
		}

		public Prueba()
		{
			//
			// Necesario para admitir el Diseñador de Windows Forms
			//
			InitializeComponent();

			//
			// TODO: agregar código de constructor después de llamar a InitializeComponent
			//
		}

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Código generado por el Diseñador de Windows Forms
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.txtArbol = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.rtxtTexto = new System.Windows.Forms.RichTextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Generar Arbol RTF";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtArbol
			// 
			this.txtArbol.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtArbol.Location = new System.Drawing.Point(16, 132);
			this.txtArbol.Multiline = true;
			this.txtArbol.Name = "txtArbol";
			this.txtArbol.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtArbol.Size = new System.Drawing.Size(632, 175);
			this.txtArbol.TabIndex = 1;
			this.txtArbol.Text = "";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(278, 16);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(160, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Ver Código RTF";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// rtxtTexto
			// 
			this.rtxtTexto.Location = new System.Drawing.Point(16, 313);
			this.rtxtTexto.Name = "rtxtTexto";
			this.rtxtTexto.Size = new System.Drawing.Size(632, 151);
			this.rtxtTexto.TabIndex = 3;
			this.rtxtTexto.Text = "";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(444, 16);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(164, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "Traducir a HTML";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Demo NRTFTree:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "Demo NRTFReader:";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(160, 56);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(278, 23);
			this.button4.TabIndex = 7;
			this.button4.Text = "Generar \"Pseudo XML\"";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(160, 94);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(112, 23);
			this.button5.TabIndex = 8;
			this.button5.Text = "Extraer Imagenes";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(26, 94);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "Otras pruebas:";
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(278, 94);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(160, 22);
			this.button6.TabIndex = 10;
			this.button6.Text = "Extraer Imagen desde Objeto";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(444, 95);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(164, 22);
			this.button7.TabIndex = 11;
			this.button7.Text = "Extraer Propiedades del RTF";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// Prueba
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 493);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.rtxtTexto);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.txtArbol);
			this.Controls.Add(this.button1);
			this.Name = "Prueba";
			this.Text = "Demo NRTFTree v0.2";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Se establecen las propiedades del cuadro de diálogo "Abrir"
			openFileDialog1.InitialDirectory = "c:\\" ;
			openFileDialog1.Filter = "Archivos RTF (*.rtf)|*.rtf|Todos los archivos (*.*)|*.*" ;
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;

            //Se muestra el cuadro de diálogo Abrir y se espera a que se seleccione un fichero RTF.
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
                //Se crea el árbol RTF.
				RtfTree arbol = new RtfTree();

                //Se carga el documento seleccionado.
				arbol.LoadRtfFile(openFileDialog1.FileName);

                //Se muestra el árbol RTF extendido en el cuadro de texto superior de la ventana.
				txtArbol.Text = arbol.ToStringEx();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Se establecen las propiedades del cuadro de diálogo "Abrir"
			openFileDialog1.InitialDirectory = "c:\\" ;
			openFileDialog1.Filter = "Archivos RTF (*.rtf)|*.rtf|Todos los archivos (*.*)|*.*" ;
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;

            //Se muestra el cuadro de diálogo Abrir y se espera a que se seleccione un fichero RTF.
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
                //Se crea el árbol RTF.
				RtfTree arbol = new RtfTree();

                //Se carga el documento seleccionado.
				arbol.LoadRtfFile(openFileDialog1.FileName);
                
                //Se muestra el código RTF del documento en el cuadro de texto superior.
				txtArbol.Text = arbol.Rtf;

                //Se muestra el RTF ya renderizado en el cuadro de texto enriquecido inferior de la ventana.
				rtxtTexto.Rtf = arbol.Rtf;
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Se establecen las propiedades del cuadro de diálogo "Abrir"
			openFileDialog1.InitialDirectory = "c:\\" ;
			openFileDialog1.Filter = "Archivos RTF (*.rtf)|*.rtf|Todos los archivos (*.*)|*.*" ;
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;

            //Se muestra el cuadro de diálogo Abrir y se espera a que se seleccione un fichero RTF.
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
                //Se crea el objeto TraductorRtf, que cargará el documento seleccionado en un árbol RtfTree.
				TraductorRtf t = new TraductorRtf(openFileDialog1.FileName);

                //Se lanza el proceso de traducción del documento RTF a formato HTML.
				txtArbol.Text = t.traducir();
			}
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Se establecen las propiedades del cuadro de diálogo "Abrir"
			openFileDialog1.InitialDirectory = "c:\\" ;
			openFileDialog1.Filter = "Archivos RTF (*.rtf)|*.rtf|Todos los archivos (*.*)|*.*" ;
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;

            //Se muestra el cuadro de diálogo Abrir y se espera a que se seleccione un fichero RTF.
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
                //Se crea el objeto TraductorXML, que creará el procesador específico "MiParser" con las instrucciones
                //para la traducción a XML del documento RTF.
				TraductorXML txml = new TraductorXML(openFileDialog1.FileName);

                //Se lanza el proceso de parsing del documento.
				txtArbol.Text = txml.traducir();
			}
		}

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Se establecen las propiedades del cuadro de diálogo "Abrir"
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Archivos RTF (*.rtf)|*.rtf|Todos los archivos (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //Se muestra el cuadro de diálogo Abrir y se espera a que se seleccione un fichero RTF.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Se crea un árbol RTF
                RtfTree arbol = new RtfTree();

                //Se carga el documento seleccionado (Este método parsea el documento y crea la estructura de árbol interna)
                arbol.LoadRtfFile(openFileDialog1.FileName);

                //Busca todos los nodos de tipo "\pict" (Imagen)
                RtfNodeCollection imageNodes = arbol.RootNode.SelectNodes("pict");

                //Se recorren los nodos encontrados
                int i = 1;
                foreach (RtfTreeNode node in imageNodes)
                {
                    //Se crea un nodo RTF especializado en imágenes
                    ImageNode imageNode = new ImageNode(node.ParentNode);

                    //Se guarda el contenido de la imagen a un fichero con el formato original con el que se almacenó en el documento.
                    imageNode.SaveImage("image" + i + "." + getExtension(imageNode.ImageFormat));

                    i++;
                }
            }
        }

        //Función auxiliar para obtener la extensión del fichero a guardar según el formato de imagen.
        private string getExtension(System.Drawing.Imaging.ImageFormat format)
        {
            string ext = "";

            if(format == ImageFormat.Png)
                ext = "png";
            else if(format == ImageFormat.Jpeg)
                ext = "jpg";
            else if(format == ImageFormat.Wmf)
                ext = "wmf";

            return ext;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Se establecen las propiedades del cuadro de diálogo "Abrir"
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Archivos RTF (*.rtf)|*.rtf|Todos los archivos (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //Se muestra el cuadro de diálogo Abrir y se espera a que se seleccione un fichero RTF.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Se crea un árbol RTF
                RtfTree arbol = new RtfTree();

                //Se carga el documento seleccionado (Este método parsea el documento y crea la estructura de árbol interna)
                arbol.LoadRtfFile(openFileDialog1.FileName);

                //Busca el primer nodo de tipo objeto.
                RtfTreeNode node = arbol.RootNode.SelectSingleNode("object");

                //Se crea un nodo RTF especializado en imágenes
                ObjectNode objectNode = new ObjectNode(node.ParentNode);

                //Se escriben al cuadro de texto superior algunos datos del objeto
                txtArbol.Text += "Object type: " + objectNode.ObjectType + "\r\n";
                txtArbol.Text += "Object class: " + objectNode.ObjectClass + "\r\n";

                //Se extrae la imagen insertada como representación del objeto

                //Se obtiene el nodo "\result" del objeto (representación externa del objeto en el documento RTF)
                RtfTreeNode resultNode = objectNode.ResultNode;

                RtfTreeNode auxNode = null;

                //Si existe un nodo imagen en el grupo "\result" la extraemos a un fichero y mostramos algunas características en
                //el cuadro de texto superior de la ventana.
                if ((auxNode = resultNode.SelectSingleNode("pict")) != null)
                {
                    //Creamos el nodo especializado de tipo Imagen
                    ImageNode imageNode = new ImageNode(auxNode.ParentNode);

                    //Mostramos algunas características de la imagen
                    txtArbol.Text += "Image width: " + imageNode.Width/20 + "\r\n";
                    txtArbol.Text += "Image heigh: " + imageNode.Height/20 + "\r\n";
                    txtArbol.Text += "Image format: " + imageNode.ImageFormat + "\r\n";

                    //Se guarda la imagen a fichero
                    MessageBox.Show("Se va a crear el fichero: image-example3." + getExtension(imageNode.ImageFormat));
                    imageNode.SaveImage("image-example3." + getExtension(imageNode.ImageFormat));
                }
                else
                { 
                    MessageBox.Show("El grupo '\result' del objeto no contiene imágenes!");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //Se establecen las propiedades del cuadro de diálogo "Abrir"
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Archivos RTF (*.rtf)|*.rtf|Todos los archivos (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //Se muestra el cuadro de diálogo Abrir y se espera a que se seleccione un fichero RTF.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Se crea un árbol RTF
                RtfTree arbol = new RtfTree();

                //Se carga el documento seleccionado (Este método parsea el documento y crea la estructura de árbol interna)
                arbol.LoadRtfFile(openFileDialog1.FileName);

                //Se obtiene la información del nodo "\info"
                InfoGroup info = arbol.GetInfoGroup();

                //Si existe el nodo de información del documento
                if (info != null)
                {
                    //Se muestran algunos datos del documento
                    txtArbol.Text += "Title: " + info.Title + "\r\n";
                    txtArbol.Text += "Subject: " + info.Subject + "\r\n";
                    txtArbol.Text += "Author: " + info.Author + "\r\n";
                    txtArbol.Text += "Company: " + info.Company + "\r\n";
                    txtArbol.Text += "Category: " + info.Category + "\r\n";
                    txtArbol.Text += "Keywords: " + info.Keywords + "\r\n";
                    txtArbol.Text += "Comments: " + info.DocComment + "\r\n";

                    txtArbol.Text += "Creation Date: " + info.CreationTime + "\r\n";
                    txtArbol.Text += "Revision Date: " + info.RevisionTime + "\r\n";

                    txtArbol.Text += "Number of Pages: " + info.NumberOfPages + "\r\n";
                    txtArbol.Text += "Number of Words: " + info.NumberOfWords + "\r\n";
                    txtArbol.Text += "Number of Chars: " + info.NumberOfChars + "\r\n";
                }
            }
        }
	}
}
