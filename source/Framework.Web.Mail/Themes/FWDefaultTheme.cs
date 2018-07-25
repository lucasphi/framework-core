using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Mail.Themes
{
    class FWDefaultTheme
    {
        public const string THEME = @"<html>
                                        <head>
	                                        <script src=""https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js""></script>
                                            <script>
                                                WebFont.load({{
                                                    google: {{ ""families"": [""Poppins:300,400,500,600,700""] }},
                                                    active: function()
                                                    {{
                                                        sessionStorage.fonts = true;
                                                    }}
                                                }});
                                            </script>
                                        </head>
                                        <body style=""margin:0; padding:0; font-family: Poppins; background-color: #f2f3f8"">

                                            <header style=""height: 50px; width: 100%; background-color: #282a3c""></header>
	                                        <div style=""margin:20px; padding: 10px 30px 30px 30px; background-color: #fff"">

                                                <h3>{0}</h3>
		                                        <div style=""text-align: justify"">
			                                        {1}
		                                        </div>
	                                        </div>
	                                        <footer style=""bottom: 0px; position: absolute; height: 30px; width: 100%; background-color: #2c2e3e""></footer>
                                        </body>
                                        </html>";
    }
}
