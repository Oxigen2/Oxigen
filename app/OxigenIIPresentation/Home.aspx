<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="OxigenIIPresentation.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
  .FooterSlide {display:block}
</style>
  <script type="text/javascript">
    /*<![CDATA[*/
    var slide_length;
    var scroll_position;
    var init_timer;

    function initScroller() {
      var innerdiv = '#ForJSSlideInner div:eq(0)'
      var inner_width = $('.FooterSlideInner img').length;
      var outer_width = 13;

      if (inner_width < outer_width) {
        $('.FooterSlide .Left').css('display', 'none')
        $('.FooterSlide .Right').css('display', 'none')
      }
      else {
        slide_length = $('#ForJSSlideInner div').width()
        $(innerdiv).clone().appendTo('#ForJSSlideInner')
        $(innerdiv).clone().prependTo('#ForJSSlideInner')
        $('#ForJSSlideInner').css('left', slide_length * -1)
        scroll_position = (slide_length * -1)
      }
      var initial_width = $('#ForJSSlideInner div').eq(0).width();
      var min_width = 950
      $(innerdiv).clone().insertAfter(innerdiv)
      $(innerdiv).clone().insertBefore(innerdiv)
      $('#ForJSSlideInner').css('left', -initial_width)
    }

    var partner_position = 0;
    var partner_timer;

    function resetScroller() {
      var scroll_width = $('#ForJSSlideInner div').eq(0).width();
      if ($('#ForJSSlideInner').css('left') >= '0px') {
        $('#ForJSSlideInner').css('left', '-1px')
      }
      else if (parseInt($('#ForJSSlideInner').css('left')) <= parseInt(-scroll_width - scroll_width)) {
        $('#ForJSSlideInner').css('left', parseInt(-(scroll_width + scroll_width) + 1) + 'px')
      }
    }
    /*]]>*/
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="HomePage">
  <img id="ForJSMonitor" width="390" height="463" src="Images/Default/monitor-frame.png" alt="" />
  <div id="FlashContent2">
    <p>Flash plug in is required to view the contents</p>
    <p><a href="http://www.adobe.com/go/getflashplayer" title="Get Adobe Flash player"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" /></a></p>
  </div>

  <script type="text/javascript">
    /*<![CDATA[*/
    swfobject.embedSWF("images/flash/home-page.swf", "FlashContent2", "365", "342",  "8.0.22", "", { screencolor: "000000", autostart: "true", controlbar: "none" }, { allowfullscreen: "true", wmode: "opaque", allowscriptaccess: "always" }, {});
    CorrectPngForIE6('ForJSMonitor');
    /*]]>*/
  </script>
  <div class="RightColumn">
    <p class="LargePara">Oxigen is a free service that turns your PC screen into a digital photo frame and allows you to automatically share content with family, friends and the world.</p>
    <p class="LargePara">Share your photos and videos by creating your own Oxigen Stream. Each time you post new content to your Stream, it will automatically appear on the screens of all your followers.</p>
    <p class="LargePara">Download the Oxigen Player and choose your personalised selection of content by following Streams from the Oxigen library.</p>
    <div class="ButtonSet">
      <div class="BigButtonGreen">
        <img id="ForJSButtonGreen1" src="Images/Default/button-green-large.png" alt="" />
        <a href="Download.aspx">Download <span>&raquo;</span></a>
        <a onclick="BlackOut('DivPopUpDownload')" class="ButtonMoreInfo" href="#">more info</a>
      </div>
      <div class="ButtonSpacerWide"></div>
      <div class="BigButtonGreen">
        <img id="ForJSButtonGreen2" src="Images/Default/button-green-large.png" alt="" />
        <a href="#" onclick="createRedirect();return false;">Create <span>&raquo;</span></a>
        <a onclick="BlackOut('DivPopUpCreate')" class="ButtonMoreInfo" href="#">more info</a>
      </div>
    </div>
    <script type="text/javascript">
      /*<![CDATA[*/
      CorrectPngForIE6('ForJSButtonGreen1');
      CorrectPngForIE6('ForJSButtonGreen2');
      /*]]>*/
    </script>
    <div class="GoogleChrome">
      <span>Oxigen is optimised for use with <a href="http://www.google.com/chrome" target="_blank">Google Chrome.</a></span>
    </div>
  </div>
  
  <div style="position:absolute; right:50px; bottom:30px; font-size:5em; color:#FFF">
    <span id="testSpan1"></span>
    <span id="testSpan2"></span>
    <span id="testSpan3"></span>
    <span id="testSpan4"></span>
    <span id="testSpan5"></span>
  </div>
  
</div>

    <div class="DivPopUp DivPopUpSmall" id="DivPopUpDownload">
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <div class="NoPaddingParas">
          <p>Download the Oxigen Player and choose the content streams you want to follow.</p>
          <p>Find streams from your:</p>
          <ul class="LineList">
            <li>friends and family</li>
            <li>favourite interests (nature, sports, wildlife etc.)</li>
            <li>favourite organisations and brands</li>
          </ul>
          <p>You can always come back later and update your stream selections.</p>
        </div>

      </div>
      <div class="DivPopBottom"></div>
    </div> 
 
    <div class="DivPopUp DivPopUpSmall" id="DivPopUpCreate">
      <div class="DivPopUpClose" title="Close"></div>
      <div class="DivPopTop"></div>
      <div class="DivPopMiddle">
        <div class="NoPaddingParas">
        <p>Create a stream: share your content (photos, videos, flash) with family, friends and the world. There are 3 easy steps:</p>
        <ol class="FatList">
          <li>UPLOAD your content (photos, videos, flash files).</li>
          <li>CONVERT your content into stylised slides.</li>
          <li>POST your slides into one or more content streams.</li>
        </ol>
        </div>
      </div>
      <div class="DivPopBottom"></div>
    </div>


</asp:Content>
