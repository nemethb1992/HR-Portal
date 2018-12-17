using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Public.templates
{
    class EmailTemplate
    {

        public string Udvozlo_Email(string name)
        {
            string content = @"
                                            <h2>Tisztelt "+ name + @"!</h2>
                                                   <p class=''>
                                                Köszönjük jelentkezését, kollegáink megkezdték pályázata feldolgozását.

                                                Amennyiben megtaláljuk az Ön számára alkalmas pozíciót, felvesszük Önnel a kapcsolatot.
                                            </p>
                                            <p>
                                                Felhívjuk figyelmét, hogy adatait 1 évig tároljuk adatbázisunkban.
                                            </p>
                                            <p Style='margin-bottom: 30px'>
                                                Amennyiben szeretné módosítani adatait vagy törölni jelentkezését, kérjük jelezze a privacy@phoenix-mecano.hu e-mail címen.
                                            </p>
                                            <p>Üdvözlettel:</p>
                                            <p>Phoenix Mecano Kecskemét Kft.</p>
                                            <p>Személyügyi Osztály</p></td>";
            return front + content + bottom;
        }
        public string Elutasito_Email(string name)
        {
            string content = @"
                                            <h2>Tisztelt " + name + @"!</h2>
                                                 <p  class=''>
                                                Köszönjük jelentkezését a Phoenix Mecano Kecskemét Kft-hez.
                                                Sajnálattal közöljük, hogy a megpályázott pozícióra nem került kiválasztásra.
                                                </p>
                                                <p>
                                                    Felhívjuk figyelmét, hogy adatait 1 évig tároljuk adatbázisunkban.
                                                </p>
                                                <p> Amennyiben szeretné módosítani adatait vagy törölni jelentkezését, kérjük jelezze a privacy@phoenix-mecano.hu e-mail címen.</p>
                                                <p Style='margin-bottom: 30px;'>További pályafutásához sok sikert kívánunk!</p>
                                                <p>Üdvözlettel:</p>
                                                <p>Phoenix Mecano Kecskemét Kft.</p>
                                                <p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }




        public string Belsos_Meghivo_Email(string name, string projekt_name, string date, string helyszin, string jeloltnev)
        {
            string content = @"
                                            <h2>Tisztelt " + name + @"!</h2>
                                            <p Style='margin-bottom: 30px;' class=''>
                                                A következő interjúd időpontja pozícióra:  <b>" + date+ @"</b>
                                                Pozíció megnevezése: neve: <b>" + projekt_name + @"</b>
                                                Jelentkező neve: <b>" + jeloltnev + @"</b>
                                                Helyszín: <b>" + helyszin + @"</b>
                                            </p>
                                            <p>Üdvözlettel:</p>
                                            <p>Phoenix Mecano Kecskemét Kft.</p>
                                            <p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }

        public string Teszt()
        {
            return @"<div align='center'>
< span class='HOEnZb'><font color = '#888888' >
 </ font ></ span >< span class='HOEnZb'><font color = '#888888' >
      </ font ></ span >< span class='HOEnZb'><font color = '#888888' >
           </ font ></ span >< table class='m_6158300992703895552MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='580' style='width:435.0pt;background:white'>
<tbody>
<tr>
<td width = '100%' style='width:100.0%;padding:0cm 0cm 0cm 0cm'>
<table class='m_6158300992703895552MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:white'>
<tbody>
<tr style = 'height:46.5pt' >
< td width='565' style='width:423.75pt;padding:0cm 0cm 0cm 0cm;height:46.5pt'>
<p class='MsoNormal'><span style = 'font-size:10.5pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#5e5e5e' >< img border='0' id='m_6158300992703895552_x0000_i1025' src='https://ci4.googleusercontent.com/proxy/gjzu1NsKeAkbadIgiNRivgc5cAhk9ss-XypmZVJw-_pHaN02SHSiE5fl7Tidn0W0oJQsIM9TCvJ-3g=s0-d-e1-ft#https://profession.hu/images/logo_3.png' class='CToWUd'><u></u><u></u></span></p>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr style = 'height:12.0pt' >
< td style='padding:0cm 0cm 0cm 0cm;height:12.0pt'></td>
</tr>
<tr>
<td width = '100%' style='width:100.0%;padding:0cm 0cm 0cm 0cm'>
<table class='m_6158300992703895552MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:white'>
<tbody>
<tr>
<td width = '100%' style='width:100.0%;padding:0cm 7.5pt 0cm 7.5pt'>
<p class='MsoNormal'><span style = 'font-size:10.5pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#5e5e5e' > Tisztelt Hirdetőnk!
<br>
<br>
Az Ön által feladott álláshirdetésre az alábbi pályázat érkezett a<a href= 'https://www.profession.hu' target= '_blank' data-saferedirecturl= 'https://www.google.com/url?q=https://www.profession.hu&amp;source=gmail&amp;ust=1544885879082000&amp;usg=AFQjCNF_XG0A3yVDqVXBFUVegUwsX4tABQ' >
< u > Profession.hu </ u ></ a > rendszerén keresztül. <br>
<br>
<b><u>Hirdetés:</u></b><br>
Pozíció/cég: <b>Takarítónő</b> (Phoenix Mecano Kecskemét KFT)<br>
Az álláshirdetés érvényessége: 2018.10.17.<br>
<a href = 'https://www.profession.hu/hu/allas/1233517' target='_blank' data-saferedirecturl='https://www.google.com/url?q=https://www.profession.hu/hu/allas/1233517&amp;source=gmail&amp;ust=1544885879082000&amp;usg=AFQjCNGgCYU_cYrH7uy6l9RDiURXz74v3Q'><u>Az álláshirdetés szövege ide kattintva olvasható</u></a><br>
<br>
<b><u>Jelentkező:</u></b><br>
Név: <b>Bugyi Zsolt</b><br>
E-mail: <b><a href = 'mailto:bugyi17@gmail.com' target= '_blank' >< u > bugyi17@gmail.com</u></a></b><br>
Telefonszám: 06202753820<br>
<br>
<b>Csatolt dokumentumok:</b><br>
zsolt önéletrajz 2018.pdf, zsolt motivációs levél 2018.pdf, <br>
<br>
Jelentkezés elküldve: <b>2018.10.12. 15:08</b> <br>
<br>
<b><u>A jelentkezéshez írt kísérőlevél:</u></b><u></u><u></u></span></p>
<div>
<p class='MsoNormal'><span style = 'font-size:10.5pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#5e5e5e' > Tisztelt Hölgyem / Uram!<br>
<br>
Ezúton szeretnék jelentkezni az Önök által, a Profession.hu oldalán meghirdetett Takarító pozícióra.
<br>
A jelentkezéshez szükséges dokumentumokat csatolva küldöm Önöknek.<br>
Ha pályázatom elnyeri tetszésüket, kérem keressenek az alábbi elérhetőségek egyikén.<br>
<br>
Üdvözlettel:<br>
Bugyi Zsolt<br>
<a href = 'mailto:bugyi17@gmail.com' target= '_blank' > bugyi17@gmail.com</a><br>
06202753820 <u></u><u></u></span></p>
</div>
<p class='MsoNormal'><span style = 'font-size:10.5pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#5e5e5e' > A
< b > jelentkező részletes adatai</b> megtekinthető a <b>Profession ATS</b> jelöltkezelő felületén:<br>
<a href = 'https://www.profession.hu/hu/munkaado/miniats/jelolt/10766098' target= '_blank' data-saferedirecturl= 'https://www.google.com/url?q=https://www.profession.hu/hu/munkaado/miniats/jelolt/10766098&amp;source=gmail&amp;ust=1544885879082000&amp;usg=AFQjCNEU-9n6g142raffzay9-Hxv-gN4tQ' > https://www.profession.hu/hu/<wbr>munkaado/miniats/jelolt/<wbr>10766098</a>
<br>
<br>
A hirdetés <b>összes jelentkezője</b> elérhető az ATS felületen ide kattintva:<br>
<a href = 'https://www.profession.hu/hu/munkaado/miniats?adv_id=1233517' target= '_blank' data-saferedirecturl= 'https://www.google.com/url?q=https://www.profession.hu/hu/munkaado/miniats?adv_id%3D1233517&amp;source=gmail&amp;ust=1544885879082000&amp;usg=AFQjCNHaHUXCIXLy8Z4BjmSYxp7fkLIntA' > https://www.profession.hu/hu/<wbr>munkaado/miniats?adv_id=<wbr>1233517</a>
<br>
<br>
Sikeres toborzást kívánunk!<br>
<a href = 'https://www.profession.hu' target= '_blank' data-saferedirecturl= 'https://www.google.com/url?q=https://www.profession.hu&amp;source=gmail&amp;ust=1544885879082000&amp;usg=AFQjCNF_XG0A3yVDqVXBFUVegUwsX4tABQ' >< u > Profession.hu </ u ></ a > < u ></ u >< u ></ u ></ span ></ p >
   </ td >
   </ tr >
   </ tbody >
   </ table >
   </ td >
   </ tr >
   < tr style= 'height:12.0pt' >
   < td style= 'padding:0cm 0cm 0cm 0cm;height:12.0pt' ></ td >
   </ tr >
   < tr >
   < td width= '100%' style= 'width:100.0%;padding:0cm 0cm 0cm 0cm' >
   < span class='HOEnZb'><font color = '#888888' >
    </ font ></ span >< span class='HOEnZb'><font color = '#888888' >
         </ font ></ span >< span class='HOEnZb'><font color = '#888888' >
              </ font ></ span >< table class='m_6158300992703895552MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%' style='width:100.0%;background:white'>
<tbody>
<tr>
<td style = 'padding:0cm 0cm 0cm 0cm' >
< p class='MsoNormal'><span style = 'font-size:10.5pt;font-family:&quot;Arial&quot;,&quot;sans-serif&quot;;color:#5e5e5e' >< img border='0' id='m_6158300992703895552_x0000_i1026' src='https://ci4.googleusercontent.com/proxy/04MdM_g3vHTMnjlYv8bStMrCoiFVG39BdCLHHO_RfJhfgTe3pXpVxziREM16Vm6kaJoMag8nr2qafqHRuQ=s0-d-e1-ft#https://profession.hu/images/lablogo_2.png' class='CToWUd'><span class='HOEnZb'><font color = '#888888' >< u ></ u >< u ></ u ></ font ></ span ></ span ></ p >< span class='HOEnZb'><font color = '#888888' >
                         </ font ></ span ></ td ></ tr ></ tbody ></ table >< span class='HOEnZb'><font color = '#888888' >
                                      </ font ></ span ></ td ></ tr ></ tbody ></ table >< div class='yj6qo ajU'><div id = ':25h' class='ajR' role='button' tabindex='0' data-tooltip='Csonkolt tartalom megjelenítése' aria-label='Csonkolt tartalom megjelenítése' aria-expanded='false'><img class='ajT' src='//ssl.gstatic.com/ui/v1/icons/mail/images/cleardot.gif'></div></div><span class='HOEnZb adL'><font color = '#888888' >
                                                     </ font ></ span ></ div > ";
        }

        public string ProjektPublikalo(string name, List<string> hirdetes, List<string> szoveg)
        {
            string hirdetesSeged = "";
            string szovegSeged = "";
            if(szoveg != null)
            {
                try
                {
                    szovegSeged = @"<b>Hirdetési szövegek:</b><br><br>
                                  Mivel bíznánk meg?<br>
                                  " + szoveg[0] + @" <br><br>
                                  Mit kínálunk ?<br>
                                  " + szoveg[1] + @" <br><br>
                                  Rád gondoltunk, ha:<br>
                                  " + szoveg[2] + @" <br><br>
                                  Előnyt élvezel, ha:<br>
                                  " + szoveg[3] + @" <br><br>";
                }
                catch (Exception)
                {
                    
                }

            }
            foreach (var item in hirdetes)
            {
                hirdetesSeged += item + "<br>";
            }
            string content = @"
                                            <h2>Tisztelt Betapress</h2>
                                            <p Style='margin-bottom: 30px;' class=''>
                                                <b>Hirdetendő projekt adatai:</b> <br>
                                                " + name + @"<br><br>
                                                <b>Hirdetési felületek:</b><br>
                                                " + hirdetesSeged + @"<br>
                                                " + szovegSeged + @"<br>

                                            </p>
                                            <p>Üdvözlettel:</p>
                                            <p>Phoenix Mecano Kecskemét Kft.</p>
                                            <p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }
        public string Jelolt_Meghivo_Email(string name, string projekt_name, string date, List<string> resztvevok)
        {
            string resztvevok_layout = "";
            foreach (var item in resztvevok)
            {
                resztvevok_layout += item + ", ";
            }
            string content = @"
                                            <h2>Tisztelt " + name + @"!</h2>
                                            <p>Telefonos egyeztetésünkre hivatkozva szeretném megerősíteni a személyes találkozó időpontját (<b>" + projekt_name+ @"</b>) pozícióra történő meghallgatás kapcsán.</p>
                                            <p>Helyszín: <b>Phoenix Mecano Kecskemét Kft. <br>6000 Kecskemét, Szent István körút 24. </b></p>
                                            <p>Időpont: <b>" + date+ @"</b></p>
                                            <p>Résztvevők: <br>" + resztvevok_layout + @"</p>
                                            <p>A portán személyi igazolvány bemutatása szükséges.</p>
                                            <p>Az interjú időtartama kb. <b>1,5  órát</b> vesz igénybe, kérjük hogy a beléptetésre tekintettel az interjú kezdete előtt 15 perccel szíveskedjék megjelenni.</p>
                                            <p Style='margin-bottom: 30px;'>Várjuk a megbeszélt időpontban!</p>
                                            <p>Üdvözlettel:</p>
                                            <p>Phoenix Mecano Kecskemét Kft.</p>
                                            <p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }
        public string Egyedi_Email(string abstractContent, string name)
        {
            string content = "<h2>Tisztelt " + name + @"!</h2><p>" + abstractContent + "</p><br><p>Üdvözlettel:</p><p>Phoenix Mecano Kecskemét Kft.</p><p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }


        string front = @"<html>
<head>
    <meta name='viewport' content='width=device-width' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>Simple Transactional Email</title>
    <style>
        img {
            border: none;
            -ms-interpolation-mode: bicubic;
            max-width: 100%;
            float: right;
        }

        body {
            background-color: #f6f6f6;
            font-family: sans-serif;
            -webkit-font-smoothing: antialiased;
            font-size: 14px;
            line-height: 1.4;
            margin: 0;
            padding: 0;
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }

        table {
            border-collapse: separate;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            width: 100%;
        }

            table td {
                font-family: sans-serif;
                font-size: 14px;
                vertical-align: top;
            }

        .body {
            background-color: #f6f6f6;
            width: 100%;
        }

        .container {
            display: block;
            Margin: 0 auto !important;
            max-width: 580px;
            padding: 10px;
            width: 580px;
        }

        .content {
            box-sizing: border-box;
            display: block;
            Margin: 0 auto;
            max-width: 580px;
            padding: 10px;
        }

        .main {
            background: #ffffff;
            border-radius: 3px;
            width: 100%;
        }

        .wrapper {
            box-sizing: border-box;
            padding: 20px;
        }

        .content-block {
            padding-bottom: 10px;
            padding-top: 10px;
        }

        .footer {
            clear: both;
            Margin-top: 10px;
            text-align: center;
            width: 100%;
        }

            .footer td,
            .footer p,
            .footer span,
            .footer a {
                color: #999999;
                font-size: 12px;
                text-align: center;
            }

        h1,
        h2,
        h3,
        h4 {
            color: #000000;
            font-family: sans-serif;
            font-weight: 400;
            line-height: 1.4;
            margin: 0;
            Margin-bottom: 30px;
        }

        h1 {
            font-size: 35px;
            font-weight: 300;
            text-align: center;
            text-transform: capitalize;
        }

        p,
        ul,
        ol {
            font-family: sans-serif;
            font-size: 14px;
            font-weight: normal;
            line-height: 170%;
            margin: 0;
            Margin-bottom: 15px;
        }

            p li,
            ul li,
            ol li {
                list-style-position: inside;
                margin-left: 5px;
            }

        a {
            color: #3498db;
            text-decoration: underline;
        }

        .btn {
            box-sizing: border-box;
            width: 100%;
        }

            .btn > tbody > tr > td {
                padding-bottom: 15px;
            }

            .btn table {
                width: auto;
            }

                .btn table td {
                    background-color: #ffffff;
                    border-radius: 5px;
                    text-align: center;
                }

            .btn a {
                background-color: #ffffff;
                border: solid 1px #3498db;
                border-radius: 5px;
                box-sizing: border-box;
                color: #3498db;
                cursor: pointer;
                display: inline-block;
                font-size: 14px;
                font-weight: bold;
                margin: 0;
                padding: 12px 25px;
                text-decoration: none;
                text-transform: capitalize;
            }

        .btn-primary table td {
            background-color: #3498db;
        }

        .btn-primary a {
            background-color: #3498db;
            border-color: #3498db;
            color: #ffffff;
        }

        .last {
            margin-bottom: 0;
        }

        .first {
            margin-top: 0;
        }

        .align-center {
            text-align: center;
        }

        .align-right {
            text-align: right;
        }

        .align-left {
            text-align: left;
        }

        .clear {
            clear: both;
        }

        .mt0 {
            margin-top: 0;
        }

        .mb0 {
            margin-bottom: 0;
        }

        .preheader {
            color: transparent;
            display: none;
            height: 0;
            max-height: 0;
            max-width: 0;
            opacity: 0;
            overflow: hidden;
            mso-hide: all;
            visibility: hidden;
            width: 0;
        }

        .powered-by a {
            text-decoration: none;
        }

        hr {
            border: 0;
            border-bottom: 1px solid #f6f6f6;
            Margin: 20px 0;
        }

        @media only screen and (max-width: 620px) {
            table[class=body] h1 {
                font-size: 28px !important;
                margin-bottom: 10px !important;
            }

            table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
                font-size: 16px !important;
            }

            table[class=body] .wrapper,
            table[class=body] .article {
                padding: 10px !important;
            }

            table[class=body] .content {
                padding: 0 !important;
            }

            table[class=body] .container {
                padding: 0 !important;
                width: 100% !important;
            }

            table[class=body] .main {
                border-left-width: 0 !important;
                border-radius: 0 !important;
                border-right-width: 0 !important;
            }

            table[class=body] .btn table {
                width: 100% !important;
            }

            table[class=body] .btn a {
                width: 100% !important;
            }

            table[class=body] .img-responsive {
                height: auto !important;
                max-width: 100% !important;
                width: auto !important;
            }
        }

        @media all {
            .ExternalClass {
                width: 100%;
            }

                .ExternalClass,
                .ExternalClass p,
                .ExternalClass span,
                .ExternalClass font,
                .ExternalClass td,
                .ExternalClass div {
                    line-height: 100%;
                }

            .apple-link a {
                color: inherit !important;
                font-family: inherit !important;
                font-size: inherit !important;
                font-weight: inherit !important;
                line-height: inherit !important;
                text-decoration: none !important;
            }

            .btn-primary table td:hover {
                background-color: #34495e !important;
            }

            .btn-primary a:hover {
                background-color: #34495e !important;
                border-color: #34495e !important;
            }
        }
    </style>
</head>
<body class=''>
    <table border='0' cellpadding='0' cellspacing='0' class='body'>
        <tr>
            <td>&nbsp;</td>
            <td class='container'>
                <div class='content'>

                    <span class='preheader'>This is preheader text. Some clients will show this text as a preview.</span>
                    <table class='main'>
                        <tr>
                            <td class='wrapper'>
                                <table border='0' cellpadding='0' cellspacing='0' Style='padding: 10px;'>
                                    <img src='https://www.phoenix-mecano.hu/wp-content/uploads/2017/02/PMK1_CMYK-300x70.png' width='215' float='right' alt='Smiley face' height='50'>
                                    <tr>
                                        <td>
                                            <p></p>";
        string bottom = @"
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <div class='footer'>
                        <table border='0' cellpadding='0' cellspacing='0'>
                            <tr>
                                <td class='content-block'>
                                    <span class='apple-link'>Phoenix Mecano Kecskemét kft.</span>
                                </td>
                            </tr>
                            <tr>
                                <td class='content-block powered-by'>
                                    Szent István krt. 24, 6000
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</body>
</html>";
    }
}
