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
 * Class:		TraductorXML
 * Description:	Clase auxiliar para llamar a MiParser (Demo NRtfTree)
 ********************************************************************************/

using System;
using Net.Sgoliver.NRtfTree.Core;
using Net.Sgoliver.NRtfTree.Util;

namespace Pruebas
{
	public class TraductorXML
	{
		private RtfReader		reader;		//Analizador para el documento RTF
		private string			rutaRTF;	//Ruta del fichero a analizar
		
		public TraductorXML(string rutaRTF)
		{
			this.rutaRTF = rutaRTF;
		}

		public string traducir()
		{
			string res = "";

			//Construimos nuestro parser
			MiParser parser = new MiParser(res);

			//Construimos el RTFReader que tratará el documento a través del SARParser creado anteriormente.
			reader = new RtfReader(parser);

			//Cargamos el fichero RTF
			reader.LoadRtfFile(rutaRTF);

			//Comenzamos el análisis del documento
			reader.Parse();

			return parser.doc;
		}
	}
}
