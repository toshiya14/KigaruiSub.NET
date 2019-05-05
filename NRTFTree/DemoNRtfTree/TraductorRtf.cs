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
 * Class:		TraductorRtf
 * Description:	Conversor simple de RTF a HTML (Demo NRtfTree)
 ********************************************************************************/

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Net.Sgoliver.NRtfTree.Core;
using Net.Sgoliver.NRtfTree.Util;

namespace Pruebas
{
	#region Clases Auxiliares y Enumeraciones

	/*****************************************************************************
	 * Clase:		EstadoRtf
	 * Autor:		Sgoliver
	 * Fecha:		13/03/2005
	 * Descripción: 
	 * ***************************************************************************/
	public class EstadoRtf
	{
		public bool negrita;		//Fuente negrita
		public bool cursiva;		//Fuente cursiva
		public bool subrayado;		//Fuente subrayada

		public string fuente;		//Tipo de fuente
		public int tamFuente;		//Tamaño de fuente
		public Color color;			//Color de fuente

		public EstadoRtf(string fue, int tam, Color col, bool neg, bool cur, bool sub)
		{
			fuente = fue;
			tamFuente = tam;
			color = col;

			negrita   = neg;
			cursiva   = cur;
			subrayado = sub;
		}
	}

	#endregion

	public class TraductorRtf
	{
		#region Atributos privados

		private RtfTree		tree;			//Analizador para el documento RTF

		private string[]	tFuentes;		//Tabla de fuentes  (array de string)
		private Color[]		tColores;		//Tabla de colores  (array de Color)

		private ArrayList	estados;		//Pila de estados del documento  (array de EstadoRtf)
		private EstadoRtf	estadoActual;	//Estado flujo RTF actual

		#endregion

		#region Constructores

		public TraductorRtf(string rutaRTF)
		{
			estados = new ArrayList();

			tree = new RtfTree();

			tree.LoadRtfFile(rutaRTF);
		}

		#endregion

		#region Métodos Públicos

		public string traducir()
		{
			string res = "";

            //Se extrae la tabla de fuentes del documento
			tFuentes = tree.GetFontTable();

            //Se extrae la tabla de colores del documento
			tColores = tree.GetColorTable();

            //Se lanza el proceso de traducción del documento a formato HTML
			res = traducirTexto();

            //Se devuelve el resultado del proceso
			return res;
		}

		public string traducirTexto()
		{
			//Cabecera del documento HTML resultante
			string res = "<html><head></head><body>";

			//Se establece el estado inicial por defecto
			estadoActual = new EstadoRtf((string)tFuentes[0],10,(Color)tColores[0],false,false,false);

			//Se aplica el formato inicial definido en 'estadoActual'
			inicioFormato();

			//Se busca el indice del primer nodo perteneciente al texto
			bool enc = false;		//Encontrado el comienzo del texto
			int i = 0;

			RtfTreeNode nodo = new RtfTreeNode();

			while(!enc && i < tree.RootNode.FirstChild.ChildNodes.Count)
			{
				nodo = tree.RootNode.FirstChild.ChildNodes[i];

				//El texto comenzará tras el primer token "\pard"
				if(nodo.NodeKey == "pard")
				{
					enc = true;
				}

				i++;
			}

			//La búsqueda anterior del primer nodo "\pard" se puede sustituir por 
			//la siguiente linea en la versión v0.2 de la librería NRtfTree:
			//

			//Se comenzará a traducir a partir del nodo en la posición 'primerNodoTexto'
			int primerNodoTexto = i - 1;

			//Inmersion
			res += traducirTexto(tree.RootNode.FirstChild, primerNodoTexto);

			//Se finaliza el estado inicial
			res += finFormato();

			//Finaliza el documento HTML
			res += "</body></html>";

			return res;
		}

		public string traducirTexto(RtfTreeNode curNode, int prim)
		{
			string res = "";

			//Grupo actual
			RtfTreeNode nprin = curNode;

			RtfTreeNode nodo = new RtfTreeNode();

			for(int i = prim; i < nprin.ChildNodes.Count; i++)
			{
				nodo = nprin.ChildNodes[i];

				if(nodo.NodeType == RtfNodeType.Group)
				{
					//Se apila el estado actual
					estados.Add(estadoActual);

					//Se crea un nueo estado inicial
					estadoActual = new EstadoRtf((string)tFuentes[0],10,(Color)tColores[0],false,false,false);	

					res += traducirTexto(nodo,0);

					//Se desapila el estado anterior
					estadoActual = (EstadoRtf)estados[estados.Count-1];
					estados.RemoveAt(estados.Count-1);
				}
				else if(nodo.NodeType == RtfNodeType.Control)
				{
					if(nodo.NodeKey == "'")
					{
						res += inicioFormato();
						res += (char)nodo.Parameter;
						res += finFormato();
					}
				}
				else if(nodo.NodeType == RtfNodeType.Keyword)
				{
					switch(nodo.NodeKey)
					{
						case "f":  //Tipo de fuente
							estadoActual.fuente = (string)tFuentes[nodo.Parameter];
							break;
						case "cf":  //Color de fuente
							estadoActual.color = (Color)tColores[nodo.Parameter];
							break;
						case "fs":	//Tamaño de fuente
							estadoActual.tamFuente = nodo.Parameter;
							break;
						case "b":	//Negrita
							if(!nodo.HasParameter || nodo.Parameter == 1) 
								estadoActual.negrita = true;
							else
								estadoActual.negrita = false;
							break;
						case "i":	//Cursiva
							if(!nodo.HasParameter || nodo.Parameter == 1) 
								estadoActual.cursiva = true;
							else
								estadoActual.cursiva = false;
							break;
						case "ul":	//Subrayado ON
							estadoActual.subrayado = true;
							break;
						case "ulnone":	//Subrayado OFF
							estadoActual.subrayado = false;
							break;
						case "par":	//Nuevo párrafo
							res += "<br>";
							break;
					}
				}
				else if(nodo.NodeType == RtfNodeType.Text)
				{
					res += inicioFormato();
					res += nodo.NodeKey;
					res += finFormato();
				}
			}

			return res;
		}

		public string inicioFormato()
		{
			string res = "";

			//Fuente (tipo, tamaño y color)
			res += "<font face='" + estadoActual.fuente + "' size='" + estadoActual.tamFuente/8 + "' color='" + toHTMLColor(estadoActual.color) + "'>";

			//Negrita, Cursiva, Subrayado
			if(estadoActual.negrita)
				res += "<b>";

			if(estadoActual.cursiva)
				res += "<i>";

			if(estadoActual.subrayado)
				res += "<u>";

			return res;
		}

		public string finFormato()
		{
			string res = "";

			//Negrita, Cursiva, Subrayado
			if(estadoActual.negrita)
				res += "</b>";

			if(estadoActual.cursiva)
				res += "</i>";

			if(estadoActual.subrayado)
				res += "</u>";

			//Fuente
			res += "</font>";

			return res;
		}

		public string toHTMLColor(Color actColor)
		{
			return "#"+intToHex(actColor.R,2)+intToHex(actColor.G,2)+intToHex(actColor.B,2);
		}

		public String intToHex(int hexint, int length)
		{
			//Convertimos el entero a hexadecimal
			string hexstr = Convert.ToString(hexint, 16);

			//Ajustamos el tamaño de la cadena
			int relleno = length - hexstr.Length;

			for(int i=0; i<relleno; i++)
				hexstr = "0" + hexstr;

			//Devolvemos la cadena
			return hexstr;
		}

		#endregion
	}
}
