<%@ Page Title="" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="FAQs.aspx.cs" Inherits="OxigenIIPresentation.FAQs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="uploader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="PageTitle">
  <div><span class="NormalTabLeft"></span><span class="NormalTabMiddle">Oxigen FAQs</span><span class="NormalTabRight"></span></div>
</div>
<div class="PanelNormalTop">
  <p>Here you can find the solutions to all your oxigen related problems</p>
</div>
<div class="PanelNormal">
  <div class="OxigenFAQ">
    <h2><a href="#">OVERVIEW</a></h2>
    <div>
      <div><a href="#OverviewWhatIs">What is Oxigen?</a></div>
      <div><a href="#OverviewHowDoes">How does Oxigen keep content up-to-date?</a></div>
      <div><a href="#OverviewWhatSort">What sort of content do people publish using Oxigen?</a></div>
      <div><a href="#OverviewHowMuch">How much does it cost to use Oxigen?</a></div>
    </div>
    <h2><a href="#">COMPATIBILITY</a></h2>
    <div>
      <div><a href="#CompatibleIs">Is Oxigen compatible with all computers?</a></div>
      <div><a href="#CompatibleWhatIs">What is the recommended PC spec for running the Oxigen Player?</a></div>
      <div><a href="#CompatibleWhatElse">What else do I need to run the Oxigen Player?</a></div>
    </div>
    <h2><a href="#">DOWNLOAD</a></h2>
    <div>
      <div><a href="#DownloadHow">How can I get Oxigen on my PC?</a></div>
      <div><a href="#DownloadWhen">When will Oxigen appear on my PC?</a></div>
      <div><a href="#DownloadChange">Can I change my stream subscriptions after installation?</a></div>
      <div><a href="#DownloadLimit">Is there a limit to the number of streams to which I can subscribe?</a></div>
      <div><a href="#DownloadPrivate">What are private / locked streams?</a></div>
      <div><a href="#DownloadWeighting">What is stream weighting?</a></div>
      <div><a href="#DownloadAdjust">How can I adjust my stream weightings?</a></div>
      <div><a href="#DownloadRemove">How do I remove a stream from my subscriptions?</a></div>
      <div><a href="#DownloadPC">What are "My PCs"?</a></div>
      <div><a href="#DownloadUninstall">How do I uninstall the Oxigen Player from my PC?</a></div>
    </div>
    <h2><a href="#">CREATE</a></h2>
    <div>
      <div><a href="#CreateHow">How can I use Oxigen to share my content?</a></div>
      <div><a href="#CreateCreate">How do I create an Oxigen stream?</a></div>
      <div><a href="#CreateContent">Which kind of content can I upload?</a></div>
      <div><a href="#CreateResolution">Which resolution should I use to upload content?</a></div>
      <div><a href="#CreateHowMuch">How much content can I upload?</a></div>
      <div><a href="#CreateSteal">Can someone steal my content?</a></div>
      <div><a href="#CreateFolders">What are the Content Folders and Slide Folders used for?</a></div>
      <div><a href="#CreateSlides">Why do I have to convert my content into slides?</a></div>
      <div><a href="#CreateTemplate">What is a Slide Template?</a></div>
      <div><a href="#CreateDelete">Why can't I delete a slide?</a></div>
      <div><a href="#CreateStreams">How do I post slides to my stream(s)?</a></div>
      <div><a href="#CreateScheduling">What Scheduling Settings can I set?</a></div>
      <div><a href="#CreateUpdate">Can I update my stream(s)?</a></div>
      <div><a href="#CreateFollow">Who will be able to follow my stream(s)?</a></div>
      <div><a href="#CreateFind">How do I, or my friends, find my stream?</a></div>
      <div><a href="#CreateMore">Can I create more than one stream?</a></div>
      <div><a href="#CreateDeleteStream">Can I delete my Stream?</a></div>
    </div>
    <h2><a href="#">TROUBLESHOOTING</a></h2>
    <div>
      <div><a href="#TroubleCannotUpdate">The content is not updating on my Oxigen Player.</a></div>   
      <div><a href="#TroubleInstalling">I'm having trouble installing the Oxigen Player.</a></div>
      <div><a href="#TroubleContent">Why is there no content showing on my screen?</a></div>
      <div><a href="#TroubleServers">Why can't my PC communicate with the Oxigen servers?  </a></div>
      <div><a href="#TroubleChanges">I've updated my stream subscriptions on the Oxigen website; why are the changes not reflected on my PC?</a></div>
      <div><a href="#TroubleFlashInstaller">I think I have Flash installed, but the Oxigen Installer is not recognising it. What is the problem?</a></div>      
      <div><a href="#TroubleSound">I have disabled sound in my screensaver but the screensaver still plays sound.</a></div>
      <div><a href="#TroubleVirus">My virus scanner says there's a virus/malware in Oxigen.</a></div>
      <div><a href="#TroubleError">Error 0xc0000135 occurs when I try to run the Oxigen installer.</a></div>
      <div><a href="#TroubleError2">Error 001 occurs when I try to run the Oxigen installer.</a></div>
    </div>
 </div>
  <script type="text/javascript">
    initFAQs();
  </script>
  <div class="FAQSection">
    <h2>Overview</h2>
    <h3 id="OverviewWhatIs">What is Oxigen? <a href="#">- Back to top</a></h3>
    <p>Oxigen is a content distribution platform that allows you to share content via the Oxigen Player. You can publish content by creating your own Oxigen stream(s) and consume content by downloading the Oxigen Player and using it to follow one or more streams.</p>
    <h3 id="OverviewHowDoes">How does Oxigen keep content up-to-date? <a href="#">- Back to top</a></h3>
    <p>Everytime a publisher posts new content to their Oxigen stream, the system automatically sends that content to everyone who is following the stream.  This all happens automatically so once you have installed the Oxigen Player and chosen your stream selection, you'll get all the latest content updates without having to lift a finger.</p>
    <h3 id="OverviewWhatSort">What sort of content do people publish using Oxigen? <a href="#">- Back to top</a></h3>
    <p>Oxigen is a publishing platform so you can use it to publish whatever content you like (as long as you respect copyright and decency).  For example, individuals can use Oxigen to share their photos with family and friends; sports teams can use Oxigen to stay in touch with their fans; businesses can use Oxigen to let customers know about their latest products and offers; museums can use Oxigen to publicise upcoming exhibitions and events; bands can use Oxigen to share videos with their fans; businesses can use Oxigen as an internal communications tool; and even politicians can use Oxigen to communicate their policies to their constituents!</p>
    <h3 id="OverviewHowMuch">How much does it cost to use Oxigen <a href="#">- Back to top</a></h3>
    <p>Nothing. The basic Oxigen service is FREE of charge both for publishers and consumers.</p>
  </div>
  <div class="FAQSpacer"></div>
  <div class="FAQSection">
    <h2>Compatibility</h2>
    <h3 id="CompatibleIs">Is Oxigen compatible with all computers? <a href="#">- Back to top</a></h3>
    <p>You can create and maintain an Oxigen stream via the Oxigen website using any web browser irrespective of your operating system.  However, in order to follow one or more Oxigen streams, you need to install the Oxigen Player.  The Oxigen Player is compatible with Windows XP, Windows Vista and Windows 7 operating systems and currently, is NOT compatible with Apple Macintosh or Linux operating systems.  We hope to release Mac and Linux versions of the Oxigen Player soon.  Any Mac or Linux programmers out there who'd like to help, do get in touch.</p>
    <h3 id="CompatibleWhatIs">What is the recommended PC spec for running the Oxigen Player? <a href="#">- Back to top</a></h3>
    <p>The Oxigen Player itself is a small, lightweight program so puts very little demand on your PC.  However, it is capable of running rich content which can put a much heavier load on your PC.  Therefore, the recommended spec depends on the type of content in the streams that you choose to follow:</p>
    <ul>
      <li>Simple photo sharing – 512MB RAM, 1GHz Pentium processor or above.</li>
      <li>Basic video content – 1GB RAM, 1.5GHz Pentium processor or above.</li>
      <li>HD video content – 2GB RAM, 2GHz dual-core processor or above.</li>
    </ul>
    <h3 id="CompatibleWhatElse">What else do I need to run the Oxigen Player? <a href="#">- Back to top</a></h3>
    <p>The Oxigen Player uses a number of helper applications to play its content.  If you do not have these installed, you can download them from the following links:</p>
    <p>Full installation requirements can be downloaded <a href="/Resources/OxigenInstallationRequirements.pdf">here</a>.</p>
    <table class="FAQCentred" cellspacing="0">
      <tr>
        <td>.NET Framework v3.5 or above</td>
        <td><a href="http://www.microsoft.com/net/download.aspx" target="_blank">www.microsoft.com</a></td>
      </tr>
      <tr>
        <td>Adobe Flash Player v10 or above</td>
        <td><a href="http://www.adobe.com/support/flashplayer/downloads.html" target="_blank">www.adobe.com</a></td>
      </tr>
      <tr>
        <td>Apple QuickTime v7 or above</td>
        <td><a href="http://www.apple.com/quicktime/download/" target="_blank">www.apple.com</a></td>
      </tr>
      <tr>
        <td>Windows Media Player v10 or above</td>
        <td><a href="http://windows.microsoft.com/en-US/windows/products/windows-media-player" target="_blank">www.microsoft.com</a></td>
      </tr>
    </table>
  </div>
  <div class="FAQSpacer"></div>
  <div class="FAQSection">
    <h2>Download</h2>
    <h3 id="DownloadHow">How can I get Oxigen on my PC? <a href="#">- Back to top</a></h3>
    <p>In order to get Oxigen on your PC you should:</p>
    <ul>
      <li>go to the Download section of the Oxigen website</li>
      <li>find the stream(s) you want to follow by browsing the categories or performing a search in the left-hand panel</li>
      <li>click on the stream thumbnail (more details will appear in the middle panel) and then click the "Add to PC" button so that the stream appears within the "My PC" section on the right-hand side</li>
      <li>repeat to add further streams if required</li>
      <li>click the "I'm Done" button which will take you to a new page with a link to an installer with your personal selection of streams pre-configured</li>
      <li>download and run your personalised installer, following the on-screen instructions</li>
    </ul>
    <h3 id="DownloadWhen">When will Oxigen appear on my PC? <a href="#">- Back to top</a></h3>
    <p>Once installed, the Oxigen Player will start up whenever your PC is idle or you can launch it yourself by right clicking the Oxigen icon in your systray (bottom right of screen) and selecting Launch Oxigen.</p>
    <h3 id="DownloadChange">Can I change my stream subscriptions after installation? <a href="#">- Back to top</a></h3>
    <p>Yes, you can edit your stream subscriptions within the Download section of the Oxigen website at any time.  Select your PC from the dropdown list in the right hand panel and the streams to which you are subscribed will display.  You can add new streams, change your stream weightings or remove streams from your subscription.  Once you have made the changes on the website and clicked "I'm Done", you can either wait 60 minutes for the changes to take place automatically or you can right click the Oxigen icon in your PC's systray (bottom right of screen) and select Update Content.</p>
    <h3 id="DownloadLimit">Is there a limit to the number of streams to which I can subscribe? <a href="#">- Back to top</a></h3>
    <p>No, there is no limit but bear in mind that your PC has to download and store all the content for those streams so if you subscribe to lots of streams, you will use more bandwidth and hard disk space.</p>
    <h3 id="DownloadPrivate">What are private / locked streams? <a href="#">- Back to top</a></h3>
    <p>The creator of the stream has decided that they only want certain people to be able to follow their stream. If a stream is private, it will be locked (padlock symbol) and you need to enter a password to gain access to the stream.  You can still add a locked stream to your subscriptions but the content will not be shown until the password is correctly entered, in which case the padlock becomes unlocked.</p>
    <h3 id="DownloadWeighting">What is stream weighting? <a href="#">- Back to top</a></h3>
    <p>Stream weighting controls the proportion of your total Oxigen playtime given over to each of the streams to which you are subscribed.  By default, each stream is given a weighting of 10 and all numbers are relative to each other.</p>
    <h3 id="DownloadAdjust">How can I adjust my stream weightings? <a href="#">- Back to top</a></h3>
    <p>In the Download section of the website, select your PC from the dropdown list in the right hand panel and the streams to which you are subscribed will display in the right hand panel. Click the stream whose weighting you wish to alter and adjust the slider that appears in the middle panel. Repeat the process for any other streams whose weighting you wish to alter and then click "I'm Done".</p>
    <h3 id="DownloadRemove">How do I remove a stream from my subscriptions? <a href="#">- Back to top</a></h3>
    <p>In the Download section of the website, select your PC from the dropdown list in the right hand panel and the streams to which you are subscribed will display in the right hand panel. To remove a stream simply click on the cross in the top right of the stream thumbnail.</p>
    <h3 id="DownloadPC">What are "My PC's"? <a href="#">- Back to top</a></h3>
    <p>Oxigen allows you to manage more than one PC from your Oxigen account.  For example, you might want to run Oxigen on both a home PC and a work PC with different stream subscriptions on each of the two PCs.  Once you have installed the Oxigen Player on both PCs, each PC will have its own profile in your "My PCs" section and you can edit your stream subscriptions for each PC individually.</p>
    <h3 id="DownloadUninstall">How do I uninstall the Oxigen Player from my PC? <a href="#">- Back to top</a></h3>
    <p>To uninstall the Oxigen Player from your PC, re-run the Oxigen Player installer.  The installer will detect your current installation and offer you the option to Uninstall.  As part of the uninstall process, that PC's profile will be removed from your "My PCs" section of the website.</p>
  </div>
  <div class="FAQSpacer"></div>
  <div class="FAQSection">
    <h2>Create</h2>
    <h3 id="CreateHow">How can I use Oxigen to share my content? <a href="#">- Back to top</a></h3>
    <p>To use Oxigen to share your content, you need to set up an Oxigen stream and post your content to it.</p>
    <h3 id="CreateCreate">How do I create an Oxigen stream? <a href="#">- Back to top</a></h3>
    <p>Creating an Oxigen stream is a simple 4-step process:</p>
    <ul>
      <li>Login / Register to your Oxigen account and go to the Create section</li>
      <li>Upload your content (photos, videos or flash files) into the Oxigen system</li>
      <li>Convert your content into slides that can be displayed by the Oxigen Player</li>
      <li>Post your slides to your stream</li>
    </ul>
    <p>By default your stream will use your name as its title but you can always Manage your Stream Properties to change the name, add a description etc if you wish.  Once completed, click on "Make It Live" and your stream will be published to the Download section of the website.</p>
    <h3 id="CreateContent">Which kind of content can I upload? <a href="#">- Back to top</a></h3>
    <p>You can upload:</p>
    <ul>
      <li>Photos: .jpg, .gif, .bmp and .png</li>
      <li>Videos: .mov, .avi, .mp4 and .wmv</li>
      <li>Flash: .swf</li>
    </ul>
    <h3 id="CreateResolution">Which resolution should I use to upload content? <a href="#">- Back to top</a></h3>
    <p>Your content will be displayed full-screen on your followers' PCs so we recommend you upload photos at a resolution between 800x600 pixels and 1920x1080 pixels.  For video content we recommend 852x450 pixels.  The system will automatically resize content so that it displays correctly on your followers' PCs.</p>
    <h3 id="CreateHowMuch">How much content can I upload? <a href="#">- Back to top</a></h3>
    <p>Standard Oxigen accounts have an allocation of 200MB of storage space and an individual file size limit of 10MB.</p>
    <h3 id="CreateSteal">Can someone steal my content? <a href="#">- Back to top</a></h3>
    <p>All content is encrypted within the Oxigen system.</p>
    <h3 id="CreateFolders">What are the Content Folders and Slide Folders used for? <a href="#">- Back to top</a></h3>
    <p>Both types of folder simply allow you to organise your content or slides so you can find them more easily.  New folders can be created, the folder name can be changed and you can move content or slides between folders.</p>
    <h3 id="CreateSlides">Why do I have to convert my content into slides? <a href="#">- Back to top</a></h3>
    <p>Slides are the objects that the Oxigen Player displays.  Thus, you need to convert your raw content into slides before posting the slides to your stream(s).  However, you can always choose to "Do Nothing" in the convert process which will preserve your content in its native form.</p>
    <h3 id="CreateTemplate">What is a Slide Template? <a href="#">- Back to top</a></h3>
    <p>Slide templates are optional styles and designs that can make your content more visually appealing when displayed by the Oxigen Player.  For example, you could use the standard Oxigen Photo Template to display your raw photos on a mounting card with a caption about the photo instead of just displaying the photo itself.  However, you do not have to use a template in the convert process, you can simply select "Do Nothing" in the convert process which will preserve your content in its native form.</p>
    <h3 id="CreateDelete">Why can't I delete a slide? <a href="#">- Back to top</a></h3>
    <p>You can only delete a slide if it is not scheduled to display in any of your streams.  To remove a slide from your stream(s), click the cross on its thumbnail image within the stream(s).  You should then be able to delete the slide itself.</p>
    <h3 id="CreateStreams">How do I post slides to my stream(s)? <a href="#">- Back to top</a></h3>
    <p>In the Create -> Post section of the website, select one or more slides in the left hand side panel (My Slides), set any scheduling settings you want to apply in the middle panel and click the Post button.  Your scheduled slides will appear as thumbnails in the right hand panel (My Streams).</p>
    <h3 id="CreateScheduling">What Scheduling Settings can I set? <a href="#">- Back to top</a></h3>
    <p>Most people just accept the defaults but you can set:</p>
    <ul>
      <li>click thru URL (the URL associated with your slide)</li>
      <li>display duration (how long your slide should display – mainly for video content)</li>
      <li>the start and end dates that your slide will display</li>
      <li>the daypart that your slide will display (e.g. only display in the morning)</li>
      <li>the days of the week that your slide will display (e.g. only display on Fridays)</li>
    </ul>
    <h3 id="CreateUpdate">Can I update my stream(s)? <a href="#">- Back to top</a></h3>
    <p>Yes, absolutely.  Whenever you post new slides to your stream(s), Oxigen automatically delivers those slides to all your followers.</p>
    <h3 id="CreateFollow">Who will be able to follow my stream(s)? <a href="#">- Back to top</a></h3>
    <p>You can control who is able to follow your stream(s) by choosing a privacy setting within your stream's properties:</p>
    <ul>
      <li>Public: anyone can follow your stream</li>
      <li>Private: followers must enter a password in order to follow your stream</li>
    </ul>
    <p>If you make your stream Private, you can also choose whether or not to receive email requests for the password to access your stream.</p>
    <h3 id="CreateFind">How do I, or my friends, find my stream? <a href="#">- Back to top</a></h3>
    <p>Once you have created your stream and clicked on "Make It Live", it will appear in the Download section of the website.  You can then either Browse or Search for your stream in the Download section.  You can also send your friends a direct link to your stream once you have clicked on "Make It Live".</p>
    <h3 id="CreateMore">Can I create more than one stream? <a href="#">- Back to top</a></h3>
    <p>Yes, you can create as many streams as you wish.  In the Create -> Post section of the website, simply click on the "Add new stream" link and enter your new stream's properties.  Use the dropdown box to select your new stream and post slides to it in the usual way.</p>
    <h3 id="CreateDeleteStream">Can I delete my Stream? <a href="#">- Back to top</a></h3>
    <p>From within the "Manage Stream Properties" there is a link to "Delete Stream". Please note, your followers may be left disappointed!</p>
  </div>
  <div class="FAQSpacer"></div>
  <div class="FAQSection">
    <h2>Troubleshooting</h2>
    <h3 id="TroubleCannotUpdate">The content is not updating on my Oxigen Player. <a href="#">- Back to top</a></h3>
    <p>Over the weekend of 12th/13th February 2011, there was a problem with the Oxigen system which may prevent your screensaver from receiving future content updates.</p>
    <p>In order to fix this problem please click on <a href="/GeneralSettings/ss_general_data.dat">this link to download the file ss_general_data.dat</a> and then follow the instructions below:</p> 
    <ol style="list-style-type:decimal">
       <li>Save the file ss_general_data.dat to your Oxigen SettingsData folder overwriting the existing version:
            <ul>
              <li style="margin-bottom:7px;">
                If you have Windows XP, the default location is: <br />
                C:\Documents and Settings\All Users\Shared Documents\Oxigen\data\SettingsData
              </li>
              <li style="margin-bottom:7px;">
                If you have Windows Vista or Windows 7, the default location is:<br />
                C:\Users\Public\Documents\Oxigen\data\SettingsData
              </li>
            </ul>
         </li>
       <li>Right-click on the Oxigen icon in your System Tray (bottom right of your screen) and select "Update Software". </li>       
       <li>Follow the instructions on screen.</li>
    </ol>
    <h3 id="TroubleInstalling">I'm having trouble installing the Oxigen Player. <a href="#">- Back to top</a></h3>
    <p>The Oxigen Player is an application that runs on your PC so you will need to be logged onto your PC with administrative privileges in order to install the software.  The installer also needs to communicate with our servers as part of the installation process so you need to be connected to the internet when you run the installer.</p>
    <h3 id="TroubleContent">Why is there no content showing on my screen? <a href="#">- Back to top</a></h3>
    <p>The first thing to check is that you are subscribed to one or more streams with content scheduled.  To do this, log into your account on the Oxigen website, go into the Download section and find your PC in the dropdown list of My PCs. Check that your PC is subscribed to at least one stream with content. If your PC is subscribed to at least one stream with content scheduled, the next thing to check is that your PC is able to communicate with the Oxigen servers in order to pull down that content.  To do this, right click the Oxigen icon in your systray (bottom right of screen) and select Update Content.  Your PC will now try to communicate with the Oxigen servers to pull down the content.  You will be able to see the progress of the communication in the popup window.  If communication between your PC and our servers is not successful, please see the FAQ about "Why can't my PC communicate with the Oxigen servers?"</p>
    <h3 id="TroubleServers">Why can't my PC communicate with the Oxigen servers? <a href="#">- Back to top</a></h3>
    <p>The Oxigen Player downloads its content from Oxigen's servers via the internet.  If you use a firewall to protect your PC, you will need to configure it to allow your PC to communicate with Oxigen's servers.  In your firewall configuration panel, you should give OxigenCE.exe, OxigenLE.exe and OxigenSU.exe access to the internet.  For example, in the Windows Firewall configuration panel, you should:</p>
    <ul>    
      <li>select the Exceptions tab</li>
      <li>click Add Program</li>
      <li>click Browse</li>
      <li>navigate to the Oxigen binaries folder (C:\Program Files\Oxigen by default)</li>
      <li>select and Add OxigenCE.exe to your Exceptions list</li>
      <li>repeat for OxigenLE.exe and OxigenSU.exe</li>
    </ul>
    <p>Oxigen communicates via HTTPS over port 443 for file extension ".svc" and for MIME type "application/octet-stream".</p>
    <h3 id="TroubleChanges">I've updated my stream subscriptions on the Oxigen website; why are the changes not reflected on my PC? <a href="#">- Back to top</a></h3>
    <p>Any change you make to your stream subscriptions will be reflected on your PC only after the Oxigen Content Exchanger has run.  This happens automatically every 60 minutes or alternatively, you can run the Content Exchanger manually by right clicking the Oxigen icon in your systray (bottom right of screen) and selecting Update Content.</p>
    <h3 id="TroubleFlashInstaller">I think I have Flash installed, but the Oxigen Installer is not recognising it. What is the problem? <a href="#">- Back to top</a></h3>
    <p>You actually need the AX version of Flash Player installed.  To get this, use Internet Explorer (32-bit if you're on a 64-bit machine), go to <a href="http://www.adobe.com">www.adobe.com</a> and download/install Flash Player from there.</p>
    <h3 id="TroubleSound">I have disabled sound in my screensaver but the screensaver still plays sound.<a href="#">- Back to top</a></h3>
    <p>We are aware of this bug (which affects Flash content) and are working on a fix for it.  As soon as we have a solution, we will push out a software update with the fix.  In the meantime, if you mute your sound at system level, it will mute the screensaver.</p>
    <h3 id="TroubleVirus">My virus scanner says there's a virus/malware in Oxigen.<a href="#">- Back to top</a></h3>
    <p>The Oxigen Player periodically communicates with our servers in order to keep your content up-to-date. Some virus scanners incorrectly identify this code as a virus/malware (a false positive). To get around the issue:</p>
    <ul>
      <li>switch your virus scanner off</li>
      <li>download the Oxigen installer</li>
      <li>run the installer and check that the digital signature identifies Oxigen II Ltd</li>
      <li>install Oxigen</li>
      <li>switch your virus scanner back on</li>
    </ul>
    <h3 id="TroubleError">Error 0xc0000135 occurs when I try to run the Oxigen installer.<a href="#">- Back to top</a></h3>
    <p>This error is usually due to a problem with your .NET installation.  Please bring your .NET installation up to date by installing each of the following updates in turn:</p>
    <p>v2.0 - <a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5" target="_blank">http://www.microsoft.com/downloads/en/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5</a></p>
    <p>v3.0 - <a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=10cc340b-f857-4a14-83f5-25634c3bf043" target="_blank">http://www.microsoft.com/downloads/en/details.aspx?FamilyID=10cc340b-f857-4a14-83f5-25634c3bf043</a></p>
    <p>v3.5 - <a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=ab99342f-5d1a-413d-8319-81da479ab0d7" target="_blank">http://www.microsoft.com/downloads/en/details.aspx?FamilyID=ab99342f-5d1a-413d-8319-81da479ab0d7</a></p>
    <p>v4.0 - <a href="http://www.microsoft.com/downloads/en/details.aspx?FamilyID=0a391abd-25c1-4fc0-919f-b21f31ab88b7" target="_blank">http://www.microsoft.com/downloads/en/details.aspx?FamilyID=0a391abd-25c1-4fc0-919f-b21f31ab88b7</a></p>
    <p>Please note that you cannot just install v4.0 as .NET is cumulative.</p>
    <h3 id="TroubleError2">Error 001 occurs when I try to run the Oxigen installer.<a href="#">- Back to top</a></h3>    
    <p>This error is usually due to an old registry key still being present on your machine. To remove the key:</p>
    <ul>
      <li>click Start->Run and type regedit into the dialogue box and then press return (this opens your registry editor)</li>
      <li>in the tree on the left expand HKEY_LOCAL_MACHINE</li>
      <li>in the tree on the left expand SOFTWARE</li>
      <li>in the tree on the left click on the Oxigen folder</li>
      <li>in the panel on the right, you should see some keys one of which will be called "Path" with a value of C:\Program Files\Oxigen</li>
      <li>right click the key "Path" and select delete</li>
      <li>confirm the delete if necessary</li>
      <li>once deleted close the registry editor</li>
      <li>re-run the Oxigen installer</li>
    </ul>
  </div>
  <div class="BottomFix"></div>
</div>
<div class="PanelNormalBottom"></div>

</asp:Content>
