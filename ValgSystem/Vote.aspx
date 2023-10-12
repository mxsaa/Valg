﻿
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vote.aspx.cs" Inherits="ValgSystem.Vote" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Voting System</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Stem på ett parti.</h1>

            <asp:Label ID="LabelParti" runat="server" Text="Velg ett parti:"></asp:Label>
            <asp:DropDownList ID="DropDownListParti" runat="server"></asp:DropDownList>

            <br />

            <asp:Label ID="LabelKommune" runat="server" Text="Velg en kommune:"></asp:Label>
            <asp:DropDownList ID="DropDownKommune" runat="server"></asp:DropDownList>

            <br />

            <asp:Button ID="ButtonVote" runat="server" Text="Stem" OnClick="ButtonVote_Click" />

            <br />

            <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>




            Del 1 - Å stemme, som i denne formen<br />
            - Kan kun stemme en gang<br />
            - Dropdown med kommune<br />
            - Dropdown fylles opp automatisk med partiene fra din db, samt kommune<br />
            - Før man kommer seg inn til denne formen, må man ha en valgkode. Slik at kun de med stemmerett kan stemme<br />
            - Valgkoden gjør så at stemmen havner i rett kommune (Da slipper brukeren å velge kommune selv).
            <br />
            - Det skal gis en feilmelding om man ikke velger et parti og klikker stem.<br />
            <br />
            Del 2 - Å vise valgresultatene<br />
            - Vise resultatene i form av en tabell. Både antall stemmer og %.
            <br />
            - Vise resultatene ved å bruke Charts<br />
            - Resultater for hele Norge (om du har kommuneversjonen i din db)<br />
            - Velge valgresultat for kommune<br />
            <br />
            Del 3 - for administratorer<br />
            - En webform for admins<br />
            - Her kan kommuner endres, slettes, legges til<br />
            - Samme for parti. Nytt parti. Endre partinavn.<br />
            - Denne siden bør ha login. Det er en Login control her i VS.
            <br />
            <br />
            Generelt<br />
            -NavneKonvensjoner<br />
            -Bruk av metoder. Feks bør ikke koden ligge i Click-metoder. De bør ligge i en egen metode som kalles.<br />
            -Settings og konfigs skal ikke hardkodes. Bruk konfigfiler.<br />
            -Databasermetoder bør ligge i en egen fil og Class. Eventuelt også i et eget prosjekt, i samme solution. Se eksempel i prosjektet her.<br />
&nbsp;&nbsp;&nbsp; Class name for databasemetoder er ofte DataBaseLayer, eller en forkortelse DBLayer, eller DAL, for Data Access Layer. Google n-tier architecture.<br />
            Websiden bør være responsive.<br />
            <br />
            Andre krav du selv kommer på!<br />
            <br />
            <img alt="homer" class="auto-style1" longdesc="monki" src="img/homermonki.gif" /></div>
    </form>
</body>
</html>
