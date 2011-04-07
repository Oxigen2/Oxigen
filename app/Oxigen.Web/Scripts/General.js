var opera = (navigator.appName.indexOf("Opera") != -1) ? true : false;
var firefox = navigator.userAgent.toLowerCase();
firefox = (firefox.indexOf("firefox") != -1) ? true : false;
var safari = navigator.userAgent.toLowerCase();
safari = (safari.indexOf("safari") != -1) ? true : false;
var chrome = navigator.userAgent.toLowerCase();
chrome = (chrome.indexOf('chrome') != -1) ? true : false;
var msie_only = (navigator.appName.indexOf("Microsoft") != -1) ? true : false;
var msie_old = false; // MSIE versions older than 7
var msie_80 = false; // MSIE version 8.0 or newer
var msie_70 = false; // MSIE vesrion 7.0
var msie_60 = false; // only MSIE 6.0
var msie_55 = false; // only MSIE 5.5

if (msie_only) {
  fullVerStr = navigator.appVersion;
  verStr = fullVerStr.substring(fullVerStr.indexOf('MSIE') + 4, fullVerStr.length);
  verStr = verStr.substring(0, verStr.indexOf(';'));
  msie_old = (parseFloat(verStr) < 7.0) ? true : false;
  msie_80 = (parseFloat(verStr) >= 8.0) ? true : false;
  msie_70 = (parseFloat(verStr) == 7.0) ? true : false;
  msie_60 = (parseFloat(verStr) == 6.0) ? true : false;
  msie_55 = (parseFloat(verStr) == 5.5) ? true : false;
}

document.writeln('<style type="text/css">\n/*<![CDATA[*/\n');

document.writeln('.WindowsTree .WindowsTreeContent {overflow-y:auto; overflow-x:hidden}\n');
if (msie_80) {
  document.writeln('body {overflow-y:scroll}\n');
  document.writeln('select {padding-bottom:3px}\n');
  document.writeln('.Radio, .CheckBox {margin-left:4px; margin-right:2px;}\n');
}

if (msie_70) {
  document.writeln('.BeneathInput2 input {top:-1px;}\n');
  document.writeln('.RbVerticalList label {top:1px; left:-7px}\n');
  document.writeln('.RbVerticalList input {left:-7px;}\n');
}

if (msie_old) {
  document.writeln("#ForJSCentreDiv         {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/centre-div-bg.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSMonitor         {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/monitor-frame.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButtonRedCentre    {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/centre-button-red.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButtonGreenCentre    {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/centre-button-green.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButtonGreen1    {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/button-green-large.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButtonGreen2    {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/button-green-large.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButton1         {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/button-big.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButton2         {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/button-big.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButton3         {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/button-bigger.png', sizingMethod='scale');}\n");
  document.writeln("#ForJSButtonSmall         {filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/Images/Default/button-small.png', sizingMethod='scale');}\n");
  document.writeln('.ContentDiv {height:451px;}\n');
  document.writeln('.RbHorizontalList input {left:-7px;}\n');
  document.writeln('.RbHorizontalList label {left:-7px;}\n');
  document.writeln('.RbVerticalList label {top:1px; left:-7px}\n');
  document.writeln('.RbVerticalList input {left:-7px;}\n');

}
if (msie_only) {
  document.writeln('.RbVerticalList {left:-6px; top:-2px;}\n');
  document.writeln('.RbVerticalList input {position:relative; top:3px}\n');
  document.writeln('.CbVerticalList {left:-6px; top:-2px;}\n');
  document.writeln('.CbVerticalList input {position:relative; top:3px}\n');
}
if (opera) {
  document.writeln('.Radio, .CheckBox {margin-left:3px; margin-right:2px;}\n');
  document.writeln('.RbHorizontalList input {}\n');
  document.writeln('.RbHorizontalList label {top:0px;}\n');
  document.writeln('.RbVerticalList {left:-1px;}\n');
  document.writeln('.RbVerticalList input {margin-top:2px; margin-bottom:3px;}\n');
  document.writeln('.CbVerticalList {left:-1px;}\n');
  document.writeln('.CbVerticalList input {margin-top:2px; margin-bottom:3px;}\n');
}
if (firefox) {
  document.writeln('.Radio, .CheckBox {margin:4px; margin-right:3px; top:2px;}\n');
  document.writeln('.RbHorizontalList label {top:-3px;}\n');
  document.writeln('.RbVerticalList input {margin-top:4px; margin-bottom:3px;}\n');
  document.writeln('.RbVerticalList label {position:relative; top:-3px; margin-left:6px;}\n');
  document.writeln('.CbVerticalList input {margin-top:4px; margin-bottom:3px;}\n');
  document.writeln('.CbVerticalList label {position:relative; top:-3px; margin-left:6px;}\n');
}
if (safari) {
  document.writeln('.Radio, .CheckBox {margin:4px; top:0px;}\n');
  document.writeln('.RbVerticalList input {margin-top:4px; margin-bottom:3px;}\n');
  document.writeln('.RbVerticalList label {top:-3px; margin-left:7px;}\n');
  document.writeln('.CbVerticalList input {margin-top:4px; margin-bottom:3px;}\n');
  document.writeln('.CbVerticalList label {margin-left:7px;}\n');
}
if (chrome) {
  document.writeln('body {overflow-y:scroll}\n');
  document.writeln('.RbVerticalList input {margin-top:4px; margin-bottom:3px;}\n');
  document.writeln('.RbVerticalList label {top:-3px; margin-left:7px;}\n');
  document.writeln('.CbVerticalList input {margin-top:4px; margin-bottom:3px;}\n');
  document.writeln('.CbVerticalList label {position:relative; top:-2px; margin-left:7px;}\n');
  document.writeln('.AccessCBList label {top:-2px;}\n');
}
document.writeln('/*]]>*/\n</style>\n');

function CorrectPngForIE6(id_or_parent_id) {
  if (msie_old) {
    var img_ref;
    if (document.getElementById(id_or_parent_id).nodeName.toLowerCase() == "img") {
      img_ref = document.getElementById(id_or_parent_id);
    } else {
      img_ref = document.getElementById(id_or_parent_id).getElementsByTagName("img")[0];
    }
    img_ref.src = "/images/spacer.gif";
  }
}

//COOKIES
var cookie_site = "Oxigen"
function toCookie(cookie_id, value) {
  var exp = new Date();
  exp.setTime(exp.getTime() + (1000 * 60 * 60 * 24 * 365));
  var cookie_name = cookie_site + "_" + cookie_id + "=" + value;
  var cookie_expires = "expires=" + exp.toGMTString();
  document.cookie = cookie_name + ";" + cookie_expires + "; path=/";
}

function toCookie2(cookie_id, value, exp_sec) { //function with cookie expire time parameter added
  var exp = new Date();
  exp.setTime(exp.getTime() + (exp_sec * 1000)); // time is in ms
  var cookie_name = cookie_site + "_" + cookie_id + "=" + value;
  var cookie_expires = "expires=" + exp.toGMTString();
  document.cookie = cookie_name + ";" + cookie_expires + "; path=/";
}

//Returns value stored in cookie with given id, or -1 if cookie didn't exist (or is expired), multi value cookies resistant.
//issue noticed: "abc" is conflicting with "abcd", any one cookie_id cannot be a part of other cookie_id
function fromCookie(cookie_id) {
  var labelName = cookie_site + "_" + cookie_id;
  var labelLen = labelName.length;
  var cookieData = document.cookie;
  var cLen = cookieData.length;
  var i = 0;
  var cEnd;
  while (i < cLen) {
    var j = i + labelLen;
    if (cookieData.substring(i, j) == labelName) {
      cEnd = cookieData.indexOf(";", j);
      if (cEnd == -1) {
        cEnd = cookieData.length;
      }
      return unescape(cookieData.substring(j + 1, cEnd));
    }
    i++;
  }
  return "-1";
}



//Limits length of text in textarea "element" (dom reference), writes information in "info" (id), counts characters
function checkLength(element, info, max_length) {
  var actual_length = element.value.length;
  var characters_left = max_length - actual_length;
  if (characters_left < 0) characters_left = 0;

  if (actual_length == 0) {
    document.getElementById(info).innerHTML = "Maximum text length is " + max_length + " characters.";
  }
  if ((actual_length > 0) && (actual_length <= max_length)) {
    document.getElementById(info).innerHTML = "You may enter " + characters_left + " more characters.";
  }
  if (actual_length > max_length) {
    element.value = element.value.substr(0, max_length);
    document.getElementById(info).innerHTML = "You may enter " + characters_left + " more characters.";
  }
}

//init a textarea, textarea_container - id, for charaacters limit
function initLengthInfo(textarea_container, info_id, max_length) {
  if (document.getElementById(textarea_container)) {
    element_ref = document.getElementById(textarea_container).getElementsByTagName("textarea")[0];
    checkLength(element_ref, info_id, max_length);
  }
}

//for search panel
function clearSearchField(field_to_clear) {
  var tmp_obj = field_to_clear.previousSibling;
  tmp_obj = tmp_obj.previousSibling;
  var initial_value = tmp_obj.value;
  if (initial_value == field_to_clear.value) field_to_clear.value = "";
}

//for search panel
function fillSearchField(field_to_fill) {
  var tmp_obj = field_to_fill.previousSibling;
  tmp_obj = tmp_obj.previousSibling;
  var initial_value = tmp_obj.value;
  if (field_to_fill.value == "") field_to_fill.value = initial_value;
}


function divAlertPopUp() {
  var body_width = $("body").width();
  if (body_width < 998) { body_width = 998; }
  var left_pos = parseInt(body_width / 2) - 176;
  $('div.DivPopUp').css('z-index', '100')
  $('div.DivPopUpAlert').css('left', left_pos).slideDown(300)
  $(window).resize(function() {
    var body_width = $("body").width();
    if (body_width < 998) { body_width = 998; }
    var left_pos = parseInt(body_width / 2) - 176;
    $('div.DivPopUpAlert').css({ left: left_pos });
  });
}
function divAlertPopUp2() {
  var body_width = $("body").width();
  if (body_width < 998) { body_width = 998; }
  var left_pos = parseInt(body_width / 2) - 176;
  $('div.DivPopUp').css('z-index', '100')
  $('div.DivPopUpAlert2').css('left', left_pos).slideDown(300)
  $(window).resize(function() {
    var body_width = $("body").width();
    if (body_width < 998) { body_width = 998; }
    var left_pos = parseInt(body_width / 2) - 176;
    $('div.DivPopUpAlert2').css({ left: left_pos });
  });
}

var button_clicked = 0;
var uploader_ready = 0;
function UploaderBlackOut(div_popup) {
  var timestamp = new Date().getTime()

  $.get('get.ox?command=getUserQuota&' + timestamp, function(data) {
    var ajax_array = new Array()
    ajax_array = data.split(',,')
    $('#DivPopUpUploader input:eq(0)').val(ajax_array[0])
    $('#DivPopUpUploader input:eq(1)').val(ajax_array[1])
    $('#DivPopUpUploader input:eq(2)').val(ajax_array[2])
    $('#spanTotalFileSize').html(bytesConverter(ajax_array[0]))
    $('#spanTotalAvailableBytes').html(bytesConverter(ajax_array[2]))

    var unformatted_curr_size = unformatFileSize(document.getElementById("spanTotalFileSize").innerHTML)
    document.getElementById("spanTotalFileSize").innerHTML = formatFileSize(parseInt($('#DivPopUpUploader input:eq(0)').val()) + parseInt(imageUploader1.getTotalFileSize()));
    imgWidth = (parseInt($('#DivPopUpUploader input:eq(0)').val()) / (parseInt($('#BytesTotal input').val()))) * 216;
    imgWidth2 = (parseInt($('#DivPopUpUploader input:eq(0)').val()) / (parseInt($('#BytesTotal input').val()))) * 216;
    document.getElementById("imgTotalFileSize").style.width = Math.round(imgWidth) + "px";
    document.getElementById("imgTotalFileSizeUsed").style.width = Math.round(imgWidth2) + "px";
    document.getElementById("spanMaxTotalFileSize").innerHTML = formatFileSize(parseInt($('#DivPopUpUploader input:eq(1)').val()));

  })



  if (uploader_ready == 1) {
    //$('#DivPopUpUploaderProgress').css('display', 'block')
    //$('.Ab').css('visibility', 'hidden')
    var body_width = $("body").width();
    if (body_width < 998) { body_width = 998; }
    var body_height = $(document).height();
    var login_pos = parseInt(body_width / 2) - 485;
    if (msie_old) $("select").css({ visibility: "hidden" });
    $("div#CoverLayer").stop(true, true)
    .css({ width: body_width, height: body_height, opacity: 0.7, display: "block" })
    $('div#' + div_popup).css({ left: login_pos, zIndex: '10000', display: 'block', width: '970px', height: '700px' });
    $(window).resize(function() {
      var body_width = $("body").width();
      if (body_width < 998) { body_width = 998; }
      var body_height = $(document).height();
      var login_pos = parseInt(body_width / 2) - 485;
      $("div#CoverLayer").stop(true, true).css({ width: body_width, height: body_height });
      $('div#' + div_popup).css({ left: login_pos });
    });
    $(window).scroll(function() {
      var body_width = $("body").width();
      if (body_width < 998) { body_width = 998; }
      var body_height = $(document).height();
      var login_pos = parseInt(body_width / 2) - 485;
      $("div#CoverLayer").stop(true, true).css({ width: body_width, height: body_height });
      $('div#' + div_popup).css({ left: login_pos });
    });
  }
  else {
    $('.UploadContentCover').css('display', 'block')
    button_clicked = 1;
  }
}
function unUploaderBlackOut(clicked) {
  var screenHor = 498;
  var screenVer = $('body').height() / 2;
  var dropdown_pos = $('#DivPopUpUploader #UploadUserFolder option').index($('#DivPopUpUploader #UploadUserFolder option:selected'))
  $('#DivPopUpUploader').css({ width: '1px', height: '1px' })
  $('div#CoverLayer').animate({ opacity: '0' }, 200, function() {
    $('div#CoverLayer').css('display', 'none')
    endProgressBar('ProgressBackHolder');

  })
  if ($(clicked).is('.DivPopUpClose')) {

  }
  else {
    force_var = 'Panel2,0'

    //convertForce() 
  }

}

function unBlackOut3(clicked) {
  var screenHor = 498;
  var screenVer = $('body').height() / 2;
  var this_div = $(clicked).closest('div.DivPopUp')
  $(clicked).closest('div.DivPopUp').slideUp(300)
  $('div#CoverLayer').animate({ opacity: '0' }, 200, function() {
    $('div#CoverLayer').css('display', 'none')
  })
  if (this_div.is('#DivLoginBox') && logged_in != 1 && window.location.href.indexOf('CreateWizard.aspx') != -1) { //IF LOGGED OUT AND ON CREATE PAGE AND CLOSING LOGIN WINDOW
    window.location = "Home.aspx"
  }
}

function BlackOut2(div_popup) {
  $('.OxiValidation').css('display', 'none')
  $('.IsChanged').html('')
  $('.ValidateField').removeClass('ValidateField')
  $('#' + div_popup).css('display', 'block')
  $('#LogEmailInput input').focus()
  $('#LogEmailInput input').closest('.RFormBox').addClass('RSelectedFormBox')
  $('#LogEmailInput input').addClass('RSelectedInput')
  var body_width = $("body").width();
  if (body_width < 998) { body_width = 998; }
  var body_height = $(document).height();
  var login_pos = parseInt(body_width / 2) - 257;
  if ($('#' + div_popup).is('.DivPopUpMedium')) {
    var login_pos = parseInt(body_width / 2) - 297;
  }
  if (msie_old) $("select").css({ visibility: "hidden" });
  $("div#CoverLayer").stop(true, true)
    .css({ width: body_width, height: body_height, opacity: 0.7, display: "block" })
  $('div#' + div_popup).css({ left: login_pos, zIndex: '10000', display: 'block' });
  $(window).resize(function() {
    var body_width = $("body").width();
    if (body_width < 998) { body_width = 998; }
    var body_height = $(document).height();
    var login_pos = parseInt(body_width / 2) - 257;
    $("div#CoverLayer").stop(true, true).css({ width: body_width, height: body_height });
    $('div#' + div_popup).css({ left: login_pos });
  });
}

function BlackOut(div_popup) {
  $('.OxiValidation').css('display', 'none')
  var body_width = $("body").width();
  if (body_width < 998) { body_width = 998; }
  var body_height = $(document).height();
  if ($('div#' + div_popup).is('div.DivPopUpSmall')) {
    var login_pos = parseInt(body_width / 2) - 257;
  }
  else if ($('div#' + div_popup).is('div.DivPopUpLarge')) {
    var login_pos = parseInt(body_width / 2) - 485;
  }
  else if ($('div#' + div_popup).is('div.DivPopUpMedium')) {
    var login_pos = parseInt(body_width / 2) - 297;
  }
  else {
    var login_pos = parseInt(body_width / 2) - 417;
  }
  if (msie_old) $("select").css({ visibility: "hidden" });
  $("div#CoverLayer").stop(true, true)
    .css({ width: body_width, height: body_height, display: "block", opacity: '0.7' })
  $('div#' + div_popup + ':not(div.MainDiv div#' + div_popup + '):eq(0)').remove()
  $('div#' + div_popup).clone(true, true).prependTo('body').css({ left: login_pos, zIndex: '10000' }).fadeIn(250, function() {
    if ($('div#' + div_popup).find('.WindowsFolder').length > 0) {
      //initDocumentView()
    }
  });
  $(window).resize(function() {
    var body_width = $("body").width();
    if (body_width < 998) { body_width = 998; }
    var body_height = $(document).height();
    if ($('div#' + div_popup).is('div.DivPopUpSmall')) {
      var login_pos = parseInt(body_width / 2) - 257;
    }
    else {
      var login_pos = parseInt(body_width / 2) - 417;
    }
    $("div#CoverLayer").stop(true, true).css({ width: body_width, height: body_height });
    $('div#' + div_popup).css({ left: login_pos });
  });
  $(window).scroll(function() {
    var body_width = $("body").width();
    if (body_width < 998) { body_width = 998; }
    var body_height = $(document).height();
    if ($('div#' + div_popup).is('div.DivPopUpSmall')) {
      var login_pos = parseInt(body_width / 2) - 257;
    }
    else {
      var login_pos = parseInt(body_width / 2) - 417;
    }
    $("div#CoverLayer").stop(true, true).css({ width: body_width, height: body_height });
    $('div#' + div_popup).css({ left: login_pos });
  });
}

function unBlackOut(clicked) {
  var screenHor = 498;
  var screenVer = $('body').height() / 2;
  $(clicked).closest('div.DivPopUp').slideUp(300, function() {
    $('div#CoverLayer').animate({ opacity: '0' }, 50, function() {
      $('div#CoverLayer').css('display', 'none')
      $(clicked).closest('div.DivPopUp').remove()
    })
  })
}

function unBlackOut2() {
  var screenHor = 498;
  var screenVer = $('body').height() / 2;
  var this_div = $('div.DivPopUp:not(#DivPopUpUploader):visible')
  this_div.slideUp('300', function() {
    $('div#CoverLayer').animate({ opacity: '0' }, 50, function() {
      $('div#CoverLayer').css('display', 'none')
      this_div.remove()
      if (this_div.is('#DivLoginBox') && logged_in != 1 && window.location.href.indexOf('CreateWizard.aspx') != -1) { //IF LOGGED OUT AND ON CREATE PAGE AND CLOSING LOGIN WINDOW
        window.location = "Home.aspx"
      }
    })
  })
}
function unBlackOut2v2() {
  var this_div = $('div.DivPopUp:not(#DivPopUpUploader):visible')
  this_div.slideUp(300)
  $('div#CoverLayer').animate({ opacity: '0' }, 200, function() {
    $('div#CoverLayer').css('display', 'none')
  })
}

function miniNavShow(div_index) {
  $('.MiniNavigation div').removeClass('Active').addClass('Inactive')
  $('.MiniNavigation div:eq(' + div_index + ')').removeClass('Inactive').addClass('Active')

  $('.MiniNavDivs').css('display', 'none')
  $('.MiniNavDivs:eq(' + div_index + ')').css('display', 'block')
  initPager('LeftColumn')
  if ($('div.MiniNavigation .Active a').html() == 'Browse') {
    var this_panel = $('div.LeftColumn .MiniNavDivs:eq(0)')
  }
  else {
    var this_panel = $('div.LeftColumn .MiniNavDivs:eq(1)')
  }
  this_panel.find('.StreamView').css('display', 'none')
  this_panel.find('.StreamView:eq(0)').css({ display: 'block', left: '14px' })


}

function removeFromPanel(panel) {
  $('.' + panel + ' .SelectedStream').remove()
  checkCentreColumn();
}
function removeFromPanel2(panel) {
  $('.' + panel + ' .SelectedStream').remove()
  checkCentreColumn2();
}


var content_length;
function streamBrowseView() {
  $('div.StreamHolder').each(function() {
    $(this).find('img:eq(0)').css('display', 'block')
  });

  $('div.RightColumn div#PersonalStream').click(function() {
    BlackOut('DivPopUpPersonal')
  });
  $('#CentreColumn div.StreamHolder div.StreamWeight').css('display', 'none')
}

function sendPasswordRequest() {
  var container = $('#ForJSPasswordRequest')
  var name = $('#JSRequestName input').val()
  var email = $('#JSRequestEmail input').val()
  var message = $('#JSRequestMessage textarea').val()
  var stream_key = $('#Unlocking span.MetaKey').html()
  var captcha = container.find('.CaptchaBox input').val()
  var ajaxstuff = 'sendRequest,,' + stream_key + ',,' + name + ',,' + email + ',,' + message + ',,' + captcha
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    if (data == -2) {
        $('#DivPopUpPrivate .OxiValidation').html('Please supply your name.').slideDown(300)
    }
    if (data == -1) {
        $('#DivPopUpPrivate .OxiValidation').html('Incorrect captcha entry. Please try again.').slideDown(300)
    }
    else if (data == 0) {
      $('#DivPopUpPrivate .OxiValidation').html('Please enter a valid email address.').slideDown(300)
    }
    else if (data == 1) {
      checkCentreColumn()
      globalClosePopUp()
    }
  })
}

function downloadClickView(clicked) {
  var stream_weight;
  if ($('div#CentreColumn div.StreamHolder').length > 0) {
    $('div#CentreColumn div.StreamHolder').remove()
    $('div.SelectedStream').removeClass('SelectedStream')
    $(clicked).addClass('SelectedStream').clone().prependTo('div#CentreColumn div.CentreContent').css('display', 'block')
  }
  else {
    $('div#CentreColumn div.StreamHolder').remove()
    $('div.SelectedStream').removeClass('SelectedStream')
    $(clicked).addClass('SelectedStream').clone().prependTo('div#CentreColumn div.CentreContent').slideDown(500)
  }
  $('#CentreColumn .CentreContent').css({ display: 'block',visibility:'visible',opacity:'1'})
  var centre_col = $('div#CentreColumn div.StreamHolder')
  centre_col.find('div.PrivateLock').parent().parent().css('opacity', '1')
  if (msie_80) { centre_col.find('div.PrivateLock').css('opacity', '1').parent().css('opacity', '1') }
  stream_weight = $(clicked).find('div.StreamMeta span.MetaWeight').html()
  var centre_meta = $('div#CentreColumn div.CentreMeta')
  var centre_slider = centre_meta.find('div.WeightingSliderInner')
  var private_meta = centre_meta.find('div.StreamPrivateMessage')
  if ($(clicked).find('.StreamMeta span.MetaPrivate').html() == 1) {
    private_meta.slideDown(300)
    $(clicked).attr('id', 'Unlocking')
    $('#DivPopUpPrivate input,#DivPopUpPrivate textarea').removeAttr('value').empty()
    if ($(clicked).find('.StreamMeta span.MetaRequest').html() == '1') {
      $('#ForJSPasswordRequest').css('display', 'block')
    }
    else {
      $('#ForJSPasswordRequest').css('display', 'none')
    }
    $('#UnlockHiddenID input').attr('value', $(clicked).find('.StreamMeta span.MetaKey').html())

    $('#DivPopUpPrivate #JSRequestMessage textarea').html('Hi, I happened to stumble upon your stream called ' + $(clicked).find('span:eq(0)').html() + '. Can you please provide me with the password to unlock it? Thanks!')
        
    BlackOut('DivPopUpPrivate')
  }
  else { private_meta.slideUp(300) }
  centre_meta.slideDown(500)
  centre_slider.slider('option', 'value', stream_weight)
  centre_slider.find('div.ui-slider-horizontal div.ui-slider-handle').append('<div class="WeightLabel">Weighting:&nbsp;<span>' + centre_slider.slider('option', 'value') + '</span></div>')
  $('#MetaWeighting').html(centre_slider.slider('option', 'value'))
  checkCentreColumn()
}

function unlockStream(clicked) {
  var stream_id = $('#UnlockHiddenID input').attr('value')
  var password = $('#PasswordCheck input').attr('value')
  var ajaxstuff = 'unlockStream,,' + stream_id + ',,' + password
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
      if (data == 0) {
          $('#DivPopUpPrivate .OxiValidation').html('Incorrect password. Please try again.').slideDown(300)
      }
      if (data == 1) {
          $('#Unlocking span.MetaPrivate').html('-2')
          $('#Unlocking').find('.PrivateLock').attr('class', 'PrivateUnlock')
          $('#Unlocking span.MetaKey').removeAttr('id')

          dropDownAjax('DropDownPCs')
          initPager('RightColumn')
          listAjax($('#BrowseNav a.BrowseMain:last').attr('id'))
          
          checkCentreColumn()
          globalClosePopUp()
      }
  })
}

var centre_checked = 0;
function checkCentreColumn() {
  $('#CentreColumn div.StreamHolder div.StreamWeight').css('display', 'none')
  var slide_speed = 500;
  var centre_col = $('#CentreColumn div.CentreContent')
  var centre_priv = centre_col.find('div.StreamPrivateMessage')
  var centre_meta = centre_col.find('div.MetaDiv')
  var weight_slide = centre_col.find('div.WeightingSlider')
  var weight_slide_label = centre_col.find('div.WeightingSliderLabel')
  var green_button = centre_col.find('div.ButtonGreen')
  var red_button = centre_col.find('div.ButtonRed')
  var already_added = centre_col.find('div.AlreadyAdded')
  right_column = $('#RightColumn'); left_column = $('#LeftColumn')
  if (centre_checked == 0) {
    centre_col.css({ opacity: '0' })
    centre_checked = 1;
  }
  if ($('div.SelectedStream:not(div#CentreColumn div.SelectedStream)').parent().parent().parent().is('#LeftColumn')) {
    red_button.css('display', 'none')
    var added_var = '';
    $('div.RightColumn div.StreamMeta span.MetaKey').each(function() {
      if ($(this).html() == centre_col.find('.StreamMeta span.MetaKey').html()) {
        added_var = 1;
      }
    });
    if (added_var != 1) {
      green_button.css('display', 'block')
      already_added.css('display', 'none')
    }
    else {
      green_button.css('display', 'none')
      already_added.css('display', 'block')
    }
  }
  else {
    green_button.css('display', 'none')
    red_button.css('display', 'block')
    already_added.css('display', 'none')
  }
  if ($('div.LeftColumn div.SelectedStream').length > 0) {
    weight_slide.slideUp(slide_speed)
    weight_slide_label.slideUp(slide_speed)
    centre_meta.slideDown(slide_speed)
  }
  else {
    centre_meta.slideUp(slide_speed)
    weight_slide.slideDown(slide_speed)
    weight_slide_label.slideDown(slide_speed)
  }
  if (left_column.find('div.SelectedStream').length == 0 && right_column.find('div.SelectedStream').length == 0) {
    
    centre_col.find('.StreamHolder').slideUp(slide_speed, function() {
      centre_col.find('.StreamHolder').remove()
    })
    centre_col.stop(true, true).animate({ opacity: '0' }, 400)
    centre_meta.slideUp(slide_speed)
    weight_slide.slideUp(slide_speed)
    weight_slide_label.slideUp(slide_speed)
    green_button.slideUp(slide_speed)
    red_button.slideUp(slide_speed)
    already_added.slideUp(slide_speed)
    centre_priv.slideUp(slide_speed)
  }
  else {
    centre_col.stop(true, true).animate({ opacity: '1' }, 400)
  }
}
function checkCentreColumn2() {
  var slide_pos = parseInt($('#SlidingPanelsInner').css('left'))
  if (slide_pos == 0) { var active_center = 0 }
  else if (slide_pos == -515) { var active_center = 1 }
  else { var active_center = 2 }
  var slide_speed = 500;
  var centre_col = $('#Center' + active_center + ' div.PanelCenterContent')
  var centre_meta = centre_col.find('div.MetaDiv')
  var weight_slide = centre_col.find('div.WeightingSlider')
  var weight_slide_label = centre_col.find('div.WeightingSliderLabel')
  var green_button = centre_col.find('div.ButtonGreen')
  var red_button = centre_col.find('div.ButtonRed')
  var already_added = centre_col.find('div.AlreadyAdded')
  var right_column;
  var left_column;
  if (slide_pos == 0) {right_column = $('#Panel2'); left_column = $('#Panel1') }
  else if (slide_pos == -515) { right_column = $('#Panel3'); left_column = $('#Panel2') }
  else { right_column = $('#Panel4'); left_column = $('#Panel3') }
  if ($('div.SelectedStream').length != 0 && left_column.is(':not(#Panel1)')) {
    if ((left_column.is('#Panel2') || left_column.is('#Panel3')) && left_column.find('div.SelectedStream').length > 0) {
      green_button.css('display', 'block')
      red_button.css('display', 'none')
    }
    else {
      red_button.css('display', 'none')
      var added_var = 0;
      right_column.find('div.StreamMeta span.MetaKey').each(function() {
        if ($(this).html() == centre_col.find('div.StreamMeta span.MetaKey').html()) {
          added_var = 1;
        }
      });
      if (added_var != 1) {
        green_button.css('display', 'block')
        already_added.css('display', 'none')
      }
      else {
        green_button.css('display', 'none')
        already_added.css('display', 'block')
      }
    }
    if (left_column.find('div.SelectedStream').length > 0) {
      weight_slide.slideUp(slide_speed)
      weight_slide_label.slideUp(slide_speed)
      centre_meta.slideDown(slide_speed)
    }
    else {
      centre_meta.slideUp(slide_speed)
      weight_slide.slideDown(slide_speed)
      weight_slide_label.slideDown(slide_speed)
    }
    if (left_column.find('div.SelectedStream').length == 0) {
      centre_col.find('div.StreamHolder').slideUp(slide_speed, function() {
        centre_col.find('div.StreamHolder').remove()
      })
      if (active_center != 1) {
        centre_col.stop(true, true).animate({ opacity: '0.3' }, 400)
      }
      centre_meta.slideUp(slide_speed)
      weight_slide.slideUp(slide_speed)
      weight_slide_label.slideUp(slide_speed)
      green_button.slideUp(slide_speed)
      red_button.slideUp(slide_speed)
      already_added.slideUp(slide_speed)
      //      $('div.TemplateChooser').slideUp(slide_speed)
      $('div.SchedulingDiv').slideUp(slide_speed)
    }
    else {
      centre_col.stop(true, true).animate({ opacity: '1' }, 400)
    }
  }
  //  if ($('div.Panel2 div.SelectedStream').length == 0) {
  //    $('div.TemplateChooser').slideUp(slide_speed)
  //  }
  $('#Center1 .PanelCenterContent').css({ display: 'block', opacity: '1' })
  $('#Center1 .PanelCenterContent div.TemplateChooser').css({ display: 'block' })
}


function streamBrowseView2() {
  $('.StreamHolder').each(function() {
    $(this).find('img:eq(0)').css('display', 'block')
  });
}

function streamClick2() {
  $('.OxiValidation').slideUp(200)
}

function multiClick2(clicked) {
  $('div.OxiValidation').slideUp(200)
  var panel = 'Panel2'
  var selected_number = $('div.' + panel + ' div.SelectedStream').length
  if (selected_number == 0 && $(clicked).closest('.Panel').is('#' + panel)) {
    $('#TemplateChooser').slideDown(500)
  }
  if ($(clicked).is('.SelectedStream')) {
    $(clicked).removeClass('SelectedStream')
  }
  else {
    $(clicked).addClass('SelectedStream')
  }
  selected_number = $('#' + panel + ' div.SelectedStream').length
  $('#StupeflixValidation span').html(selected_number)
  if (selected_number <= 15) {  //&& selected_number > 4
    $('#TemplateButton').css('display', 'block')
    $('#StupeflixValidation').css('color', '#090')
    $('#StupeflixValidation span').css('color', '#090')
  }
  else {
    if ($('#TemplateChooser option').index($('#TemplateChooser option:selected')) == 2) {
      $('#TemplateButton').css('display', 'none')
    }
    $('#StupeflixValidation').css('color', '#C00')
    $('#StupeflixValidation span').css('color', '#C00')
  }
  var slide_pos = parseInt($('#SlidingPanelsInner').css('left'))
  if (slide_pos == 0) { var active_center = 0 }
  else if (slide_pos == -515) { var active_center = 1 }
  else { var active_center = 2 }
  var panel_var = active_center
  if (selected_number == 1) {
    if ($(clicked).is('.SelectedStream')) {
      $('#Center' + panel_var + ' .PanelCenterContent .StreamHolder').remove()
      $(clicked).addClass('SelectedStream').clone().prependTo('#Center' + panel_var + ' .PanelCenterContent')
    }
    else {
      $('#Center' + panel_var + ' .PanelCenterContent .StreamHolder').remove()
      $('div.Panel2 div.SelectedStream').clone().prependTo('#Center' + panel_var + ' .PanelCenterContent')
    }
  }
  else {
      $('#Center' + panel_var + ' .PanelCenterContent .StreamHolder').addClass('MultipleStream')
  }
  $('#Center' + panel_var + ' .CentreMeta div.MetaDiv').css('display', 'none')
  checkCentreColumn2()
}

var init_multi_sched_var = 0
function multiClick3(clicked) {
  $('div.OxiValidation').slideUp(200)
  var panel = 'Panel3'
  var selected_number = $('#' + panel + ' div.SelectedStream').length
  if (selected_number == 0) {
    $('div.SchedulingDiv').slideDown(500)
  }
  if ($(clicked).is('div.SelectedStream')) {
    $(clicked).removeClass('SelectedStream')
  }
  else {
    $(clicked).addClass('SelectedStream')
  }
  selected_number = $('#' + panel + ' div.SelectedStream').length
  var slide_pos = parseInt($('#SlidingPanelsInner').css('left'))
  if (slide_pos == 0) { var active_center = 0 }
  else if (slide_pos == -515) { var active_center = 1 }
  else { var active_center = 2 }
  var panel_var = active_center
  if (selected_number == 0) {
    init_multi_sched_var = 0;
  }
  else if (selected_number == 1) {
  if ($(clicked).is('.SelectedStream')) {
      $('#Center' + panel_var + ' div.PanelCenterContent div.StreamHolder').remove()
      $(clicked).addClass('SelectedStream').clone().prependTo('#Center' + panel_var + ' div.PanelCenterContent')
      var content_id = $(clicked).find('div.StreamMeta span.MetaKey').html()
    }
    else {
      $('#Center' + panel_var + ' div.PanelCenterContent div.StreamHolder').remove()
      $('#Panel3 div.SelectedStream').clone().prependTo('#Center' + panel_var + ' div.PanelCenterContent')
      var content_id = $('#Panel3 div.SelectedStream').find('div.StreamMeta span.MetaKey').html()
    }
    var timestamp = new Date().getTime()
    var todays_date = new Date()
    var this_month = (todays_date.getMonth() + 1)
    if (this_month < 10) { this_month = '0' + this_month }
    $.get(ajax_path_get + '?command=getSlideProperties&slideID=' + content_id + '&' + timestamp, function(data) {
      var ajax_str = data
      ajax_array = ajax_str.split('||')
      var final_array = new Array;
      for (var x = 0; x < ajax_array.length; x++) {
        final_array[x] = ajax_array[x].split(',,')
      }
      $('#MultiSchedURL').val(final_array[0][4])
      $('#MultiSchedDur').val(final_array[0][5])
      if (init_multi_sched_var == 0) {
        $('#MultiSchedSD2 input').val(todays_date.getDate() + '/' + this_month + '/' + todays_date.getFullYear())
        $('#MultiSchedED2 input').val(todays_date.getDate() + '/' + this_month + '/' + (todays_date.getFullYear() + 2))
        //$('#MultiSchedST').val('00:00')
        //$('#MultiSchedET').val('23:59')
        $('#MultiSchedDays input').attr('checked', 'checked')
        init_multi_sched_var = 1
      }
    });
  }
  else {
    $('#Center' + panel_var + ' div.PanelCenterContent div.StreamHolder').addClass('MultipleStream')
    $('#MultiSchedURL').val('[Slide URL]')
    $('#MultiSchedDur').val('[Slide Duration]')
  }

  checkCentreColumn2()
}

function templateChooser() {
  var panel = 'Panel2'
  $('#TemplateChooser').change(function () {
      var hidden_selection = $('div.TemplateChooser .HiddenSelection')
   //   hidden_selection.slideUp(300)
      var selected_option = $('#TemplateChooser option').index($('#TemplateChooser option:selected'))

      if (selected_option == 0)
          hidden_selection.slideUp(300);
      else
          hidden_selection.slideDown(300);
  });
}

function chooseClickType(clicked) {
  if (window.location.href.indexOf('CreateWizard.aspx') == -1) {
    downloadClickView(clicked)    
  }
  else {
    var stream_clicked = $(clicked).closest('div.Panel')
    if (whatpanel == 1 && stream_clicked.is('#Panel2')) {
      multiClick2(clicked)
    }
    else if (whatpanel == 2 && stream_clicked.is('#Panel3')) {
      multiClick3(clicked)
    }
  }
}

function convertContent() {
    var select_value = $('.TemplateChooser option:selected').val()

    if (select_value == 0) {
        var ajaxstuff = 'templateConvert,,' + $('#ForJQSlideDropDown option:selected').val() + ',,0,,' + $('#Template1Input1').val() + ',,' + $('#Template1Input2').val() + ',,'
    }
    else  {
        var ajaxstuff = 'templateConvert,,' + $('#ForJQSlideDropDown option:selected').val() + ',,' + select_value + ',,' + $('#ConvertTemplateCaption').val() + ',,' + $('#ConvertTemplateCredit').val() + ',,'
    }

  $('#Panel2 .SelectedStream').each(function() {
    ajaxstuff = ajaxstuff + $(this).find('.MetaKey').html() + '||'
  });
  ajaxstuff = ajaxstuff.substring(0, (ajaxstuff.length - 2))
//  alert(ajaxstuff)
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {

  })
}


function imageCarousel() {
  if ($('.SelectedStream .StreamMeta span.MetaPrivate').html() != 1) {
    $('.CentreColumn .StreamHolder img').fadeOut(300)
    $('.CentreColumn .StreamHolder img:eq(' + preview_image + ')').fadeIn(300)
    if (preview_image == (content_length - 1)) {
      preview_image = 0;
    }
    else {
      preview_image++
    }
    preview_timer = setTimeout('imageCarousel()', 2000)
  }
}
function initWeightSlider() {
  var weight_target = '.SelectedStream:not(.CentreColumn .SelectedStream) .StreamWeight'
  $(".WeightingSliderInner").slider({
    value: 10,
    min: 1,
    max: 100,
    step: 1,
    start: function(event, ui) { $(this).find('.WeightLabel').css('display', 'block') },
    slide: function(event, ui) {
      $(this).find('.WeightLabel span').html(ui.value)
      $('#MetaWeighting').html(ui.value)
    },
    stop: function(event, ui) {
      $('.SelectedStream:not(.CentreColumn .SelectedStream) .StreamMeta span.MetaWeight').html(ui.value)
      $(this).find('.WeightLabel').css('display', 'none')
      $(weight_target).html(ui.value)
      if (ui.value == 100) {
        $(weight_target).css({ fontSize: '1em', paddingTop: '3px', height: '18px', paddingLeft: '2px', width: '20px' })
      }
      else if (ui.value < 10) {
        $(weight_target).css({ fontSize: '1.4em', paddingTop: '1px', height: '20px', paddingLeft: '8px', width: '14px' })
      }
      else {
        $(weight_target).css({ fontSize: '1.4em', paddingTop: '1px', height: '20px', paddingLeft: '3px', width: '19px' })
      }
      var pc_id = $('#DropDownPCs select option:selected').attr('value')
      var channel_id = $('.SelectedStream:not(.CentreColumn .SelectedStream) span.MetaKey').html()
      var ajaxstuff = 'channelWeighting,,' + pc_id + ',,' + channel_id + ',,' + ui.value
      //alert(ajaxstuff)
      $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
        //alert(data)
      })

    }
  });
  $('.ui-slider-horizontal .ui-slider-handle').append('<div class="WeightLabel">Weighting:&nbsp;<span>' + $(".WeightingSliderInner").slider('option', 'value') + '</span></div>')
}

function initPersonal(panel) {
  //    var personal_config = '<div class="StreamHolder PersonalStream" id="PersonalStream"><div class="StreamImages"><img src="Images/Default/channel-personal.gif" alt="Your personal stream" /></div><span>Your personalised stream</span><div class="StreamMeta"><span class="MetaWeight">10</span></div></div>'
  //    $('div.' + panel + ' div.StreamView:eq(0)').prepend(personal_config)
}

function initPrivacy(panel) {
  $('.' + panel + ' .StreamMeta').each(function() {
    if ($(this).find('span.MetaPrivate').html() == 1) {
      $(this).parent().find('.StreamImages').prepend('<div class="PrivateLock"></div>')
    }
    if ($(this).find('span.MetaPrivate').html() == 2) {
      $(this).parent().find('.StreamImages').prepend('<div class="PrivateUnlock"></div>')
    }
  });
  $('.RightColumn .StreamHolder').css('opacity', '1')
  $('.RightColumn .StreamHolder .PrivateLock').parent().parent().css('opacity', '0.4')
  if (msie_80) {
    $('.RightColumn .StreamHolder .PrivateUnlock').css('opacity', '1').parent().css('opacity', '1')
    $('.RightColumn .StreamHolder .PrivateLock').css('opacity', '0.4').parent().css('opacity', '0.4') 
  }
}
function cloneStream(original, destination) {
  if ($('div.RightColumn .StreamView').length == 0 && destination == 'RightColumn') {
    $('div.RightColumn div.PanelInputFields').after('<div class="StreamView"></div>')
  }
  var selected_option = $('#TemplateChooser option').index($('#TemplateChooser option:selected'))
  var dest_div = $('div.' + destination)
  var last_stream_view = dest_div.find('div.StreamView:last')
  var orig_div = $('div.' + original)
  var selected_stream = orig_div.find('div.StreamView div.SelectedStream')
  if (dest_div.attr('class') == 'Panel Panel3') {
    addContentAjax('ForJQSlideDropDown')
  }
  else if (dest_div.attr('class') == 'Panel Panel4') {
    addContentAjax('ForJQStreamDropDown')
  }
  if (selected_option < 2 || dest_div.attr('class') != 'Panel Panel3') {
    if (destination == 'RightColumn') {
      var left_position = '2px'
    }
    else {
      var left_position = '14px'
    }
    var visible_page = dest_div.find('.StreamView:visible')

    selected_stream.each(function() {
      if (last_stream_view.find('.StreamHolder').length == 12) {
        last_stream_view.after('<div class="StreamView"></div>')
        last_stream_view = dest_div.find('div.StreamView:last')
        last_stream_view.css('display', 'none')
        initPager(destination)
      }
      $(this).removeClass('SelectedStream')
      $(this).clone(true, true).appendTo(last_stream_view)
      if (visible_page.is(':not(div.' + destination + ' div.StreamView:last)')) {
        visible_page.animate({ left: '-350px' }, 600, function() {
          $(this).css('display', 'none')
        })
        last_stream_view.css({ display: 'block', left: '350px' }).animate({ left: left_position }, 600, function() {

        })
      }
    });
    $('div.RightColumn div.StreamHolder div.PrivateLock').parent().parent().css('opacity', '0.4')
    if (msie_80) { $('div.RightColumn div.StreamHolder div.PrivateLock').css('opacity', '0.4').parent().css('opacity', '0.4') }

  }
  else if (selected_option == 2) {
    var snapshot = $('div.SelectedStream:first img:first').attr('src')
    var left_position = '14px'
    var visible_page = dest_div.find('.StreamView:visible')
    if (last_stream_view.find('.StreamHolder').length == 12) {
      last_stream_view.after('<div class="StreamView"></div>')
      last_stream_view = dest_div.find('div.StreamView:last')
      last_stream_view.css('display', 'none')
      initPager(destination)
    }
    var selected_streamlength = $('div.Panel2 div.SelectedStream').length
    if (selected_streamlength >= 5) {
      last_stream_view.append('<div onclick="chooseClickType(this)" class="StreamHolder StupeFlix"><div alt="Remove" title="Remove" class="StreamRemove"></div><div class="StupeflixOverlay"></div><div class="StreamImages"><img src="' + snapshot + '" alt="" /></div></div>')
      last_stream_view.find('div.StupeFlix:last').bind('click', streamClick2)
      last_stream_view.find('div.StupeFlix:last div.StreamRemove').bind('click', stupeflixRemove)
      selected_stream.removeClass('SelectedStream')
    }
    if (visible_page.is(':not(div.' + destination + ' div.StreamView:last)')) {
      visible_page.animate({ left: '-350px' }, 600, function() {
        $(this).css('display', 'none')
        updatePager()
      })
      last_stream_view.css({ display: 'block', left: '350px' }).animate({ left: left_position }, 600)
    }
    $('div.RightColumn div.StreamHolder div.PrivateLock').parent().parent().css('opacity', '0.4')
    if (msie_80) { $('div.RightColumn div.StreamHolder div.PrivateLock').css('opacity', '0.4').parent().css('opacity', '0.4') }
  }
  dest_div.find('.PagerPages .PageSelected').removeClass('PageSelected')
  dest_div.find('.PagerPages span:last').addClass('PageSelected')
  $('#ProgressBackHolder').css('display', 'block')
  $('div.CreateArrowLeft,div.CreateArrowRight').css('opacity', '0.2')
  checkCentreColumn();

  createMemory()
  $('#ProgressBackHolder').css('display', 'block')
  if (destination == 'Panel3') {
    setTimeout("createDropDownAjax(1,true)", 200)
  }
  else if (destination == 'Panel4') {
    setTimeout("createDropDownAjax(2,true)", 500)
  }
  $('#ProgressBackHolder').css('display', 'none')
  $('#CreateArrowLeft,#CreateArrowRight').css('opacity', '1')
}

function reloadCreate() {
  createMemory()
  createDropDownAjax()
  $('#ProgressBackHolder').css('display', 'none')
  $('div.CreateArrowLeft,div.CreateArrowRight').css('opacity', '1')
}

function stupeflixRemove() {
  var parent_div = $(this).closest('div.Panel').attr('class').charAt(11)
  $(this).addClass('REMOVEME')
  if ($(this).parent().parent().find('.StreamHolder').length == 1) {
    $(this).parent().parent().remove()
    initPager('Panel' + parent_div)
    $('.Panel' + parent_div + ' .StreamView:last').css({ display: 'block', left: '-350px' }).animate({ left: '14px' }, 300)
  }
  searchRemove2(this, 'Panel' + parent_div)
  $(this).parent().remove()
  checkCentreColumn2();
}


var content_dropdown_pos;
var content_pager_pos;
var content_pager_pos_last;
var slide_dropdown_pos;
var slide_pager_pos;
var stream_dropdown_pos;
var stream_pager_pos;
function createMemory() {
  //var hidden_input = $('#CreateHiddenFields input')
  content_dropdown_pos = $('#Panel2 select:eq(0) option').index($('#Panel2 select:eq(0) option:selected'))
  content_pager_pos = $('#Panel2 .PagerPages span').index($('#Panel2 .PagerPages span.PageSelected'))
  slide_dropdown_pos = $('#Panel3 select:eq(0) option').index($('#Panel3 select:eq(0) option:selected'))
  slide_pager_pos = $('#Panel3 div.PagerPages span').index($('#Panel3 div.PagerPages span.PageSelected'))
  stream_dropdown_pos = $('#Panel4 select:eq(0) option').index($('#Panel4 select:eq(0) option:selected'))
  stream_pager_pos = $('#Panel4 .PagerPages span').index($('#Panel4 .PagerPages span.PageSelected'))
  //alert(slide_pager_pos)
}

function uploaderFix(clicked) {
  content_dropdown_pos = $(clicked).find('option').index($(clicked).find('option:selected'))
}



function addCrosses() {
  $('.RightColumn .StreamView .StreamHolder').each(function() {
    if ($(this).find('.StreamRemove').length == 0) {
      $(this).prepend('<div alt="Remove" title="Remove" onclick="removeContentAjax(this)" class="StreamRemove StreamIcon"></div>')
    }
  });
  $('div.StreamHolder div.StreamIcon').css('opacity', '0.5')
  stopPropagation()
}
//function addCrosses2() {
//  $('div.Panel div.StreamView div.StreamHolder').each(function() {
//    if ($(this).find('.StreamRemove').length == 0) {
//      $(this).prepend('<div alt="Delete Content" title="Delete Content" onclick="removeContentAjax(this)" class="StreamRemove StreamIcon"></div>')
//    }
//  });
//  stopPropagation()
//}
function addCrosses2() {
  //alert($('div.Panel div.StreamView div.StreamHolder .StreamRemove').length)
  $('div.Panel div.StreamView div.StreamHolder .StreamRemove').css({display:'block'});
  //stopPropagation()
}

function addTimings() {
  $('div.Panel4 div.StreamHolder:not(.StreamThumbnail) div.StreamTimings').css('display','block');
}

function addPreview() {
  var curr_url = window.location.href
  if (curr_url.indexOf('CreateWizard.aspx') != -1) {
    $('div.Panel div.StreamView div.StreamHolder:not(.StreamThumbnail)').css('display','block')
  }
  else if (curr_url.indexOf('Download.aspx') != -1) {

  }
  else if (curr_url.indexOf('ChannelDetails.aspx') != -1) {
    //alert('222')
    $('.StreamHolder').each(function() {
      if ($(this).find('div.StreamPreview').length == 0) {
        $(this).prepend('<div onclick="popPreview(this)" alt="Preview" title="Preview" class="StreamPreview StreamIcon"></div>')
      }
    });
  }
  $('div.StreamHolder div.StreamIcon').css('opacity', '0.5')
}

function showIcons() {
  $('div.StreamHolder:not(.HoverEffectAdded)').hover(function() {
    $(this).find('.StreamIcon').stop(true, true).animate({ opacity: '1' }, 200)
  }, function() {
    $(this).find('.StreamIcon').animate({ opacity: '0.5' }, 200)
  });
  $('div.StreamHolder').addClass('HoverEffectAdded')
}

function popPreview(clicked) {
  var content_title = $(clicked).parent().find('.StreamImages').next('span').text()
  $('#StreamPreviewTitle').html(content_title)
  var content_type;
  if (window.location.href.indexOf('CreateWizard.aspx') != -1) {
    if ($(clicked).closest('.Panel').attr('id') == 'Panel2') {
      content_type = 'R' //Raw Content
    }
    else if ($(clicked).closest('.Panel').attr('id') == 'Panel3') {
      content_type = 'S' //Slide
    }
    else {
      content_type = 'C' //Stream
    }
    var content_id = $(clicked).parent().find('div.StreamMeta span.MetaKey').html()
  }
  else if (window.location.href.indexOf('Download.aspx') != -1) {

  }
  else if (window.location.href.indexOf('ChannelDetails.aspx') != -1) {
    content_type = 'C' //Stream
    var content_id = $(clicked).parent().find('img:first').attr('class')
  }
  var timestamp = new Date().getTime()
  //alert(ajax_array[1])
  $.get(ajax_path_get + '?command=getPreview&type=' + content_type + '&ID=' + content_id + '&' + timestamp, function(data) {
    //alert(data)
    //var data = 'A,,http://iis6-server/Client Sites/OxigenIIAdvertisingSystem/AssetFiles/AssetContents/A/312be6ac-fad7-4d89-9231-b0b54da4a432_A.JPG'
    //var data = 'F,,images/flash/home-page.swf'
    var ajax_array = new Array()
    ajax_array = data.split(',,')
    var final_array = new Array()
    final_array = ajax_array[1].split('||')
    if (ajax_array[0] == 'A') {
      if (final_array.length > 1) {
        $('#divStreamPreview #divStreamPreviewContent').empty().append('<div class="PopPreviewImageHolder"></div>')
        var img_str = ''
        for (var v = 0; v < final_array.length; v++) {
          img_str = img_str + '<img src="' + final_array[v] + '" alt="" />'
        }
        $('#divStreamPreview .PopPreviewImageHolder').append(img_str)
        popPreviewSlides()
        $('#divStreamPreview #divStreamPreviewContent img:eq(0)').load(function() {
          positionPreview();
        });
      }
      else {
        //alert(ajax_array[1])
        $('#divStreamPreview #divStreamPreviewContent').empty().append('<img src="' + ajax_array[1] + '" alt="" />')
        $('#divStreamPreview #divStreamPreviewContent img').css('display', 'block')
        $('#divStreamPreview #divStreamPreviewContent img').load(function() {
          positionPreview();
        });
      }
    }
    else {
      $('#divStreamPreview #divStreamPreviewContent').empty().append('<div id="divPreviewFlash"></div>')
      swfobject.embedSWF(ajax_array[1], "divPreviewFlash", "900", "600", "8.0.22", "", { screencolor: "000000", autostart: "true", controlbar: "none" }, { allowfullscreen: "true", wmode: "opaque", allowscriptaccess: "always" }, {});
      positionPreview();
    }

  });

}

function positionPreview() {
  var div_width = $('#divStreamPreview').width()
  if (div_width > 950) { div_width = 950 }
  var body_width = $("body").width();
  if (body_width < 998) { body_width = 998; }
  var body_height = $(document).height();
  var login_pos = parseInt(body_width / 2) - (div_width / 2);
  if (msie_old) $("select").css({ visibility: "hidden" });
  $("div#CoverLayer").stop(true, true)
        .css({ width: body_width, height: body_height, opacity: 0.7, display: "block" })
  $('#divStreamPreview').css({ width: div_width, left: login_pos }).fadeIn(300);
  $('#divStreamPreview img').css({ width: (div_width - 40) });
  //$('#divStreamPreview div.StreamPreviewClose').css('width', (div_width - 20))
  $(window).resize(function() {
    var body_width = $("body").width();
    if (body_width < 998) { body_width = 998; }
    var body_height = $(document).height();
    var login_pos = parseInt(body_width / 2) - (div_width / 2);
    $("div#CoverLayer").stop(true, true).css({ width: body_width, height: body_height });
    $('#divStreamPreview').css({ left: login_pos }).fadeIn(300);
  });
}

var pop_preview_timer;
var pop_preview_pos = 0
function popPreviewSlides() {
  var img_holder = $('#divStreamPreview .PopPreviewImageHolder')
  var img_holder_imgs = img_holder.find('img')
  img_holder_imgs.css('display', 'none')
  img_holder_imgs.eq(pop_preview_pos).css('display', 'block')
  if (pop_preview_pos < (img_holder_imgs.length - 1)) {
    pop_preview_pos++
  }
  else {
    pop_preview_pos = 0
  }
  pop_preview_timer = setTimeout("popPreviewSlides()", 3000)
}

function unStreamPreview() {
  var div_width = $('#divStreamPreview').width()
  clearTimeout(pop_preview_timer)
  $('#divStreamPreview').fadeOut(200, function() {
    $('div.CoverLayer').fadeOut(200)
    $('div.StreamPreviewClose').css('display', 'none')
  })
}


function searchRemove(clicked) {
  var stream_view = $(clicked).parent().parent().parent().find('div.StreamView')
  if (stream_view.length > 1) {
    var streamview_length = stream_view.length
    var this_streamview = stream_view.index($(clicked).parent().parent()) + 1
    if (this_streamview < streamview_length) {
      next_stream_view = $(clicked).parent().parent().next('div.StreamView')
      var first_stream = next_stream_view.find('div.StreamHolder:first')
      first_stream.clone(true, true).appendTo($(clicked).parent().parent())
      first_stream.remove()
      if (next_stream_view.find('div.StreamHolder').length == 11 && next_stream_view.next('div.StreamView').length == 1) {
        searchRemove(next_stream_view.find('div.StreamRemove:first'))
      }
      if (next_stream_view.find('.StreamHolder').length == 0) {
        next_stream_view.remove()
        initPager('RightColumn')
      }
    }
  }
  else if ($(clicked).parent().is('.ButtonRed')) {
    var last_stream = $('div.RightColumn div.StreamView:last')
    if (last_stream.find('div.StreamHolder').length == 1) {
      last_stream.remove()
      last_stream = $('div.RightColumn div.StreamView:last')
      last_stream.css({ display: 'block', left: '-350px' }).animate({ left: '2px' }, 600)
      initPager('RightColumn')
      updatePager()
    }
    clicked = $('div.RightColumn div.SelectedStream div.StreamRemove')
    searchRemove(clicked)
  }
}
function searchRemove2(clicked, panel) {
  var last_stream = $('div.' + panel + ' div.StreamView:last')
  if (last_stream.find('div.StreamHolder').length == 1) {
    last_stream.remove()
    //last_stream = $('div.' + panel + ' div.StreamView:last')
    //last_stream.css({ display: 'block', left: '-350px' }).animate({ left: '14px' }, 600)
    initPager(panel)
    updatePager(panel)
  }
  clicked = $('div.' + panel + ' div.REMOVEME')
  searchRemove(clicked)
}

function addWeight() {
  $('.RightColumn .StreamView .StreamHolder').each(function() {
    var meta_weight = $(this).find('.StreamMeta .MetaWeight').html()
    if ($(this).find('.StreamWeight').length == 0) {
      $(this).prepend('<div class="StreamWeight"></div>')
    }
    $(this).find('.StreamWeight').html(meta_weight)
    if (isShowWeight() == '1') {
      showWeighting()
    }
  });
}

function addProperties(panel) {
  $('div.' + panel + ' div.StreamView div.StreamHolder div.StreamProperties').css('display','block');
  $('div.' + panel + ' div.BindMeP').bind('click', { area: panel }, function(event) {
    $(this).addClass('PropertyEditing')
    if (event.data.area == 'Panel2') {
      $('#HiddenContentPanel input').attr('value', 'Panel2')
      $('#ContentPropertiesTitle span').html('Content')
      var command_type = 'getRawContent'
      var content_id = $(this).parent().find('div.StreamMeta span.MetaKey').html()
      var id_type = 'contentID'
    }
    else if (event.data.area == 'Panel3') {
      $('#HiddenContentPanel input').attr('value', 'Panel3')
      $('#ContentPropertiesTitle span').html('Slide')
      var command_type = 'getSlideProperties'
      var content_id = $(this).parent().find('div.StreamMeta span.MetaKey').html()
      var id_type = 'slideID'
    }

    $('#DivPopUpContentProperties #HiddenContentID').html(content_id)
    var timestamp = new Date().getTime()
    $('#DivPopUpContentProperties input.TextBox:not(#HiddenContentPanel input)').each(function() { $(this).removeAttr('value') });
    $.get(ajax_path_get + '?command=' + command_type + '&' + id_type + '=' + content_id + '&' + timestamp, function(data) {
      var ajax_str = data
      ajax_array = ajax_str.split('||')
      var final_array = new Array;
      for (var x = 0; x < ajax_array.length; x++) {
        final_array[x] = ajax_array[x].split(',,')
      }
      for (var z = 0; z < $('#DivPopUpContentProperties input.TextBox').length; z++) {
        if (z == 4) {
          final_array[0][z] = unMicEncode(final_array[0][z])
        }
        $('#DivPopUpContentProperties input.TextBox:eq(' + z + ')').val(final_array[0][z])
      }

    });
    BlackOut2('DivPopUpContentProperties')
  })
  $('div.' + panel + ' div.BindMeP').removeClass('BindMeP')
}
function updateProperties(window) {
  if ($('#DivPopUpContentProperties #HiddenContentPanel input').val() == 'Panel2') {
    var command_type = 'putRawContent'
  }
  else {
    var command_type = 'editSlideProperties'
  }
  var content_id = $('#DivPopUpContentProperties #HiddenContentID').html()
  var ajaxstuff = command_type + ',,' + content_id + ',,' + $('#RCTitle').val() + ',,' + $('#RCCreator').val() + ',,' + $('#RCCaption').val() + ',,' + $('#RCDate2 .TextBox').val() + ',,' + micEncode($('#RCURL').val()) + ',,' + $('#RCDuration').val()
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    $('#DivPopUpContentProperties input.ValidateField').removeClass('ValidateField')
    if (data == 1) {
      $('div.PropertyEditing').removeClass('PropertyEditing').parent().find('span:eq(0)').html($('#RCTitle').val())
      unBlackOut2v2()
      //createDropDownAjax()
    }
    else {
      if (data == -4) {
        $('#ContentPropertyValidation').html('Please enter a title').slideDown(300)
        $('#RCTitle').addClass('ValidateField')
      }
      else if (data == -1) {
        $('#ContentPropertyValidation').html('Please enter a valid date in the format DD/MM/YYYY').slideDown(300)
        $('#RCDate').addClass('ValidateField')
      }
      else if (data == -2) {
        $('#ContentPropertyValidation').html('Please enter a valid duration. Must be a number').slideDown(300)
        $('#RCDuration').addClass('ValidateField')
      }
      else if (data == -3) {
        $('#ContentPropertyValidation').html('Display duration is out of bounds.').slideDown(300)
        $('#RCDuration').addClass('ValidateField')
      }
    }

  })
}


var pager_max = 13;
function initPager(panel) {
  if (panel == 'LeftColumn' && $('div.MiniNavigation .Active a').html() == 'Browse') {
    var this_panel = $('div.' + panel + ' .MiniNavDivs:eq(0)')
  }
  else if (panel == 'LeftColumn') {
    var this_panel = $('div.' + panel + ' .MiniNavDivs:eq(1)')
  }
  else {
    var this_panel = $('div.' + panel)
  }
  var stream_view = this_panel.find('div.StreamView')
  var stream_number = stream_view.length
  var pager_width = 48;
  var pages_width = 0;
  var pages_width_outer = 0;
  var pager_nav = this_panel.find('div.PagerNav')
  if (stream_number == 0) {
    pager_nav.css('display', 'none')
    return false;
  }
  else {
    pager_nav.css('display', 'block')
  }
  pager_nav.clone().insertAfter(pager_nav)
  pager_nav.eq(0).remove()
  pager_nav = $('div.' + panel + ' div.PagerNav')
  var pager_pages = pager_nav.find('div.PagerPages')

  pager_pages.empty()
  stream_view.eq(0).css('display', 'block')
  if (stream_number >= 1) {
    for (var i = 1; i <= stream_number; i++) {
      pager_pages.append('<span>' + i + '</span>')
      if (i < pager_max + 1) {
        pager_width = pager_width + 14
        pages_width_outer = pages_width + 14
      }
      pages_width = pages_width + 14
    }
    pager_nav.css({ display: 'block', width: pager_width + 'px' })
    pager_pages.css({ width: pages_width + 'px' })
    pager_nav.find('div.PagerPagesOuter').css({ width: pages_width_outer + 'px' })
  }
  pager_pages.find('span:eq(0)').addClass('PageSelected')

  var left_position;
  pager_nav.find('*').click(function() {
    if (panel == 'RightColumn') {
      left_position = '2px'
    }
    else {
      left_position = '14px'
    }
  })
  pager_pages.find('span').click(function() {
    if (panel == 'LeftColumn' && $('div.MiniNavigation .Active a').html() == 'Browse') {
      var this_panel = $('div.' + panel + ' .MiniNavDivs:eq(0)')
    }
    else if (panel == 'LeftColumn') {
      var this_panel = $('div.' + panel + ' .MiniNavDivs:eq(1)')
    }
    else {
      var this_panel = $('div.' + panel)
    }
    var stream_view = this_panel.find('div.StreamView')
    var stream_number = stream_view.length
    var page_number = $(this).html()
    var pager_page = stream_view.eq((page_number - 1))
    var visible_page = this_panel.find('div.StreamView:visible')
    if (stream_view.index(visible_page) != (page_number - 1)) {
      if (stream_view.index(visible_page) > (page_number - 1)) {
        visible_page.animate({ left: '350px' }, 300, function() {
          $(this).css('display', 'none')
          updatePager();
        })
        pager_page.css({ display: 'block', left: '-350px' }).animate({ left: left_position }, 300)
      }
      else {
        visible_page.animate({ left: '-350px' }, 300, function() {
          $(this).css('display', 'none')
          updatePager();
        })
        pager_page.css({ display: 'block', left: '350px' }).animate({ left: left_position }, 300)
      }
    }
  });
  pager_nav.find('span.PagerStart').click(function() {
    var visible_page = this_panel.find('div.StreamView:visible')
    if (stream_view.index(visible_page) != 0) {
      visible_page.animate({ left: '350px' }, 300, function() {
        $(this).css('display', 'none')
        updatePager();
      })
      stream_view.eq(0).css({ display: 'block', left: '-350px' }).animate({ left: left_position }, 300)
    }
    //createMemory()
  });
  pager_nav.find('span.PagerEnd').click(function() {
    var visible_page = this_panel.find('div.StreamView:visible')
    var last_page = this_panel.find('div.StreamView:last')
    if (stream_view.index(visible_page) != stream_view.index(last_page)) {
      visible_page.animate({ left: '-350px' }, 300, function() {
        $(this).css('display', 'none')
        updatePager();
      })
      last_page.css({ display: 'block', left: '350px' }).animate({ left: left_position }, 300)
    }
  });
  pager_nav.find('span.PagerNext').click(function() {
    var visible_page = this_panel.find('div.StreamView:visible')
    var last_page = this_panel.find('div.StreamView:last')
    if (stream_view.index(visible_page) != stream_view.index(last_page)) {
      visible_page.animate({ left: '-350px' }, 300, function() {
        $(this).css('display', 'none')
        updatePager();
      })
      stream_view.eq((stream_view.index(visible_page) + 1)).css({ display: 'block', left: '350px' }).animate({ left: left_position }, 300)
    }
  });
  pager_nav.find('span.PagerPrevious').click(function() {
    var visible_page = this_panel.find('div.StreamView:visible')
    var first_page = stream_view.eq(0)
    if (stream_view.index(visible_page) != stream_view.index(first_page)) {
      visible_page.animate({ left: '350px' }, 300, function() {
        $(this).css('display', 'none')
        updatePager();
      })
      stream_view.eq((stream_view.index(visible_page) - 1)).css({ display: 'block', left: '-350px' }).animate({ left: left_position }, 300)
    }
  });

}

function updatePager() {
  $('div.PagerNav span.PageSelected').removeClass('PageSelected')
  $('div.PagerNav').each(function() {
    var pager_pages = $(this).find('div.PagerPages')
    var pager_page_span = pager_pages.find('span')
    var stream_views = $(this).parent().find('div.StreamView')
    var page_number = stream_views.length
    var visible_page = stream_views.index($(this).parent().find('div.StreamView:visible'))
    pager_page_span.eq(visible_page).addClass('PageSelected')
    if (pager_page_span.index($(this).find('div.PagerPages span.PageSelected')) > Math.floor((pager_max / 2))) {
      if (pager_page_span.index($(this).find('div.PagerPages span.PageSelected')) < (pager_page_span.length - Math.floor((pager_max / 2)))) {
        var pager_pos = (visible_page - Math.floor((pager_max / 2)))
        var move_length = (pager_pos * -14) + 'px'
        pager_pages.animate({ left: move_length })
      }
      else {
        var move_length = (pager_pages.parent().width() - pager_pages.width()) + 'px'
        pager_pages.animate({ left: move_length })
      }
    }
    else {
      var move_length = 0 + 'px'
      pager_pages.animate({ left: move_length })
    }
  });
}

var panel_var;
var whatpanel;
function initArrowNav() {
  panel_var = 0;
  var arrow_divs = $('#NavigationArrows div')
  arrow_divs.eq(0).find('a').css({ color: '#FFF' }).parent().find('img:eq(0)').css('display', 'block')
  arrow_divs.eq(1).css({ zIndex: '2', left: '-20px' }).find('img:eq(1)').css('display', 'block')
  arrow_divs.eq(2).css({ zIndex: '1', left: '-40px' }).find('img:eq(1)').css('display', 'block')
  whatpanel = 0;
}
function updateArrowNav(active_arrow) {
  $('#Panel' + (active_arrow + 2) + ' div.StreamHolder').css('cursor', 'default')
  $('#Panel' + (active_arrow + 1) + ' div.StreamHolder').css('cursor', 'pointer')
  whatpanel = active_arrow
  var arrow_divs = $('#NavigationArrows div')
  var sliding_panel = $('#SlidingPanelsInner')
  var next_button = $('#CreateNextButton')
  arrow_divs.each(function() {
    $(this).find('img').css({ display: 'none' }).parent().find('img:eq(1)').css('display', 'block').parent().find('a').css('color', '#8EB6D8')
  })
  arrow_divs.eq(0).css({ zIndex: '3' })
  arrow_divs.eq(1).css({ zIndex: '2' })
  arrow_divs.eq(2).css({ zIndex: '1' })
  arrow_divs.eq(active_arrow).find('img').css('display', 'none').parent().find('img:eq(0)').css('display', 'block').parent().find('a').css('color', '#FFF').parent().css('zIndex', '4')
  if (active_arrow == 0) {
    $('#CreateArrowRight').removeClass('CreateArrowRight2')
    $('#CreateArrowLeft').addClass('CreateArrowLeft2')
    sliding_panel.animate({ left: '0px' }, 500, function() {
      next_button.html('Next  <span>&raquo;</span>')
    })
    $('#IntroText').html('Upload your content (photos, videos and Flash files), edit their properties and organise your content into folders.')
  }
  if (active_arrow == 1) {
    $('#CreateArrowLeft').removeClass('CreateArrowLeft2')
    $('#CreateArrowRight').removeClass('CreateArrowRight2')
    sliding_panel.animate({ left: '-515px' }, 500, function() {
      next_button.html('Next  <span>&raquo;</span>')
    })

    $('#IntroText').html('Select Content on left, choose a template in the middle and Convert your Content into Slides on right.')
  }
  if (active_arrow == 2) {
    $('#CreateArrowLeft').removeClass('CreateArrowLeft2')
    $('#CreateArrowRight').addClass('CreateArrowRight2')
    sliding_panel.animate({ left: '-1030px' }, 500, function() {
      next_button.html('Finished<span>&nbsp;</span>')
    })
    arrow_divs.eq(1).css({ zIndex: '3' })
    $('#IntroText').html('Select which Slides you want to show in your Stream(s) and when you want them to show.')
  }
  sliding_panel.find('div.SelectedStream').removeClass('SelectedStream')
  checkCentreColumn2()
}

function initBrowseNav() {
  if (window.location.href.indexOf('Download.aspx') != -1) {
    var nav_holder = $('#BrowseNav div.NavHolder')
  }
  else {
    var nav_holder = $('#DivPopUpStreamProperties:first #BrowseNav div.NavHolder')
  }
  var browse_number = nav_holder.length
  nav_holder.stop(true, true)
  for (var i = 1; i < 8; i++) {
    nav_holder.eq((7 - i)).css('zIndex', i)
  }
  if (browse_number <= 3) {
    nav_holder.removeClass('ClosedNav').addClass('OpenNav')
  }
  else {
    $('div#BrowseNav div.NavHolder:lt(' + (browse_number - 3) + ')').removeClass('OpenNav').addClass('ClosedNav').find('img').attr('src', 'Images/Default/browse-nav-closed.png')
    $('div#BrowseNav div.NavHolder:gt(' + (browse_number - 4) + ')').removeClass('ClosedNav').addClass('OpenNav')
  }
  var closed_nav = $('div#BrowseNav div.ClosedNav')
  var open_nav = $('div#BrowseNav div.OpenNav')
  closed_nav.animate({ width: '31px' }, nav_speed)
  closed_nav.find('.BrowseMain').animate({ width: '0px', paddingLeft: '31px' }, nav_speed)
  closed_nav.find('img').animate({ width: '31px' }, nav_speed)
  open_nav.find('.BrowseMain').animate({ width: '96px', paddingLeft: '15px' }, nav_speed)
  open_nav.find('img').attr('src', 'Images/Default/browse-nav-open.png')
  open_nav.find('img').animate({ width: '111px' }, nav_speed)
  open_nav.animate({ width: '111px' }, nav_speed)
}

// must change this back to the big server's URL when testing is done - Michali - DONE - Ryan
var ajax_path_get = '/get.ox'
var ajax_path_put = '/put.ox'

function AjaxTest() {
  $.get(ajax_path_get + '?command=pcs', function(data) {
    var ajax_str = data
    $('#AJAXTEST').html(ajax_str)
  })
}

function createAccountAjax() {
  var email = $('#ExistingUser:visible input[type=text]:eq(0)').val()
  var password = $('#ExistingUser:visible input[type=password]:eq(0)').val()
  var firstname = $('#ExistingUser:visible input[type=text]:eq(1)').val()
  var lastname = $('#ExistingUser:visible input[type=text]:eq(2)').val()
  var ajaxstuff = 'signup,,' + email + ',,' + password + ',,' + firstname + ',,' + lastname
  if ($('#ExistingUserTerms').is(':checked') && $('#ExistingUser:visible input[type=password]:eq(1)').val() == password) {
    $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
      //alert(data)
      if (data == 1) {
        $('#HeaderLoggedIn2').css('display', 'inline').prev('span').css('display', 'inline')
        $('#HeaderLoggedIn2').next('span').css('display', 'inline')
        $('#HeaderLoggedIn').css('display', 'inline').prev('span').css('display', 'none')
        $('#HeaderLoggedIn span').html(firstname)
        $('#HeaderLoggedOut').css('display', 'none')

        unBlackOut2();
      }
      else if (data == -1) {
        $('#ExistingUser input').each(function() {
          if ($(this).attr('value').length < 1) {
            $(this).addClass('ValidateField')
          }
          else {
            $(this).removeClass('ValidateField')
          }
        });
        $('#DivLoginBox div.OxiValidation:eq(0)').slideUp(200)
        $('#ExistingUserValidation').html('Please fill out the highlighted boxes to proceed').slideDown(200)
      }
      else if (data == -2) {
        $('#ExistingUser input[type=text]:eq(0)').addClass('ValidateField')
        $('#DivLoginBox div.OxiValidation:eq(0)').slideUp(200)
        $('#ExistingUserValidation').html('This Email Address is already used. <a href="ForgottenPassword.aspx" title="Forgotten Password">Click here</a> to retrieve your password.').slideDown(200)
      }
      else if (data == -3) {
        $('#ExistingUser input[type=text]:eq(0)').addClass('ValidateField')
        $('#DivLoginBox div.OxiValidation:eq(0)').slideUp(200)
        $('#ExistingUserValidation').html('Please enter a valid Email Address.').slideDown(200)
      }
      $('#ProgressLogin').css({ display: 'none' })

    })
  }
  else {
    if ($('#ExistingUserTerms').is(':not(:checked)')) {
      $('#DivLoginBox div.OxiValidation:eq(0)').slideUp(200)
      $('#ExistingUserValidation').html('Please agree to the Oxigen terms and conditions.').slideDown(200)
      $('#ProgressLogin').css({ display: 'none' })
    }
    else {
      $('#DivLoginBox div.OxiValidation:eq(0)').slideUp(200)
      $('#ExistingUserValidation').html('Passwords do not match.').slideDown(200)
      $('#ProgressLogin').css({ display: 'none' })
    }
  }
}


function shuffle(array) {
  var tmp, current, top = array.length;
  if (top) while (--top) {
    current = Math.floor(Math.random() * (top + 1));
    tmp = array[current]; array[current] = array[top];
    array[top] = tmp;
  }
  return array;
}

function addFolderAjax() {
  var folder_length = $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.WindowsFolder').length
  for (var g = 0; g < folder_length; g++) {
    var old_folder_name = 'New Folder ' + g
    if ($('.' + old_folder_name).length == 0) {
      var new_folder_name = 'New Folder ' + g
    }
  }
  var active_screen = $('.ManageScreenOpen').parent().parent().parent().attr('id')
  if (location.href.indexOf('Download.aspx') == -1) {
    if (active_screen == 'Panel2') {
      ajax_command = 'addAssetContentFolder'
    }
    else if (active_screen == 'Panel3') {
      ajax_command = 'addSlideFolder'
    }
    else {
    }
  }
  else {
    ajax_command = 'addPC'
  }
  var ajaxstuff = ajax_command + ',,' + new_folder_name
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    //alert(data)
    $('.FolderOpen').removeClass('FolderOpen')
    $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.WindowsFolder:last').removeClass().addClass('AjaxFolder' + data).addClass('WindowsFolder').addClass('FolderOpen')
    $('div.WindowsView div.WindowsHolder').css('display', 'none')
    $('div.WindowsView div.WindowsHolder:last').css('display', 'block')
  })
}

function addPCAjax(){
  var ajaxstuff = 'addPC,,Temporary PC'
  var dropdown = 'DropDownPCs'
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    var timestamp = new Date().getTime()
    $.get(ajax_path_get + '?command=pcs&' + timestamp, function(data) {
      var ajax_str = data
      var ajax_error = 0;
      if (ajax_str.length == 0) {
        ajax_error = 1
      }
      ajax_array = ajax_str.split('||')
      var final_array = new Array;
      for (var x = 0; x < ajax_array.length; x++) {
        final_array[x] = ajax_array[x].split(',,')
      }
      $('#' + dropdown + ' select').empty()
      if (ajax_error == 0) {
        for (var r = 0; r < ajax_array.length; r++) {
          var pc_name = final_array[r][1]
          var pc_id = final_array[r][0]
          var pc_phantom = final_array[r][2]
          var container = '\'RightColumn\''
          $('#' + dropdown + ' select').append('<option value="' + pc_id + '">' + pc_name + '</option>')
          $('#' + dropdown + ' select option:last').data('phantom', pc_phantom)
        }
        var last_option = $('#' + dropdown + ' select option:last').attr('value')
        $('#' + dropdown + ' select option:last').attr('selected','selected')
        streamlistAjax(last_option, 'RightColumn', '1', '4')
      }
      else {
        var pc_id = '-1'
        $('#' + dropdown + ' select').append('<option value="' + pc_id + '" onfocus="streamlistAjax(' + pc_id + ',' + container + ', 1)">My PC</option>')
      }
    })
  })
}

function removeFolderAjax() {
  var active_screen = $('.ManageScreenOpen').parent().parent().parent().attr('id')
  //alert(active_screen)
  if (active_screen == 'Panel2') {
    ajax_command = 'removeAssetContentFolder'
  }
  else if (active_screen == 'Panel3') {
    ajax_command = 'removeSlideFolder'
  }
  else {
    ajax_command = 'removePCFolder'
  }
  var folder_id = $('.FolderOpen').attr('class').substring(10, $('.FolderOpen').attr('class').indexOf(' '))
  var ajaxstuff = ajax_command + ',,' + folder_id
  //alert(ajaxstuff)
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    //alert(data)
    if (data != -1) {
      $('div.WindowsHolder').css('display', 'none')
      var selected_index = $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.WindowsFolder').index($('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.FolderOpen'))
      $('div.WindowsView:visible div.WindowsHolder:eq(' + selected_index + ')').remove()
      $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.FolderOpen').remove()
      $('div.WindowsView:visible div.RFormBox input').removeAttr('value')
      $('#ContentFolderName').empty()
    }
    else {
      $('div.WindowsView:visible .OxiValidation').html('Please unschedule content before removing folder.').slideDown(300)
    }
  })
}

function setWeightingAjax(new_weight) {
  var pc_id = $('#DropDownPCs option:selected').val()
  var content_id = $('#CentreColumn div.SelectedStream span.MetaKey').html()
  var ajaxstuff = 'channelWeighting ,,' + pc_id + ',,' + content_id + ',,' + new_weight
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    //alert(data)
  })
}

function addContentAjax(dropdown_id) {
  //alert(whatpanel)
  var pc_id = $('#' + dropdown_id + ' option:selected').val()
  //alert($('#' + dropdown_id).html())
  //Question: (AC) What if streams are selected in pannel 1 and panel 2?
  var stream_str = ''
  $('.SelectedStream:not(#CentreColumn .SelectedStream,#PanelCenter .SelectedStream,.PanelCenter .SelectedStream)').each(function() {
    stream_str += $(this).find('span.MetaKey').html() + '||'
  });
  stream_str = stream_str.substring(0, (stream_str.length - 2))
  if (location.href.indexOf('Download.aspx') == -1) {
      if (whatpanel == '1') {
          //ajax_command = 'addSlideContents'
          //****No longer required.  convertContent() will add slide contents
          return;
      }
    else {
        ajax_command = 'addChannelContents'
    }
  }
  else {
    ajax_command = 'addPCStream'
  }
  var ajaxstuff = ajax_command + ',,' + pc_id + ',,' + stream_str;
  //alert(ajaxstuff)
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    //alert(data)
  })
}

function stopPropagation() {
  $('.StreamRemove:not(.PropStopped)').each(function() {
    $(this).addClass('PropStopped')
    $(this).click(function(event) {
      $('.OxiValidation').slideUp(200)
      event.stopPropagation();
    });
  });
}

function removeContentAjax(clicked) {
  var dropdown_id;
  var parent_class = $(clicked).parent().parent().parent();
  var col_pos = $(clicked).closest('.StreamView').parent().attr('class')
  //alert(col_pos)
  if (parent_class.is('.RightColumn')) {
    dropdown_id = 'DropDownPCs'
    ajax_type = 'removePCStream'
  }
  else {
    if (col_pos == 'Panel Panel2') {
      dropdown_id = 'DropDownFolders'
      ajax_type = 'removeRawContents'
    }
    else if (col_pos == 'Panel Panel3') {
      dropdown_id = 'ForJQSlideDropDown'
      ajax_type = 'removeSlideContents'
    }
    else if (col_pos == 'Panel Panel4') {
      dropdown_id = 'ForJQStreamDropDown'
      ajax_type = 'removeChannelContents'
    }
  }
  var pc_id = $('#' + dropdown_id + ' option:selected').val()
  var stream_str = $(clicked).parent().find('span.MetaKey').html()
  //alert(stream_str)

  var ajax_type;

  var ajaxstuff = ajax_type + ',,' + pc_id + ',,' + stream_str;
  //alert(ajaxstuff)
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    //alert(data)
    if (data != -1) {
      if (window.location.href.indexOf('CreateWizard.aspx') != -1) {
        var parent_div = $(clicked).closest('div.Panel').attr('class').charAt(11)
        $(clicked).addClass('REMOVEME')
        if ($(clicked).parent().parent().find('.StreamHolder').length == 1) {
          $(clicked).parent().parent().remove()
          initPager('Panel' + parent_div)
          $('.Panel' + parent_div + ' .StreamView:last').css({ display: 'block', left: '-350px' }).animate({ left: '14px' }, 300)
          //$('.Panel' + parent_div + ' .PagerPages span:last').click()
          $('.Panel' + parent_div + ' .PagerPages .PageSelected').removeClass('PageSelected')
          $('.Panel' + parent_div + ' .PagerPages span:last').addClass('PageSelected')
        }
        searchRemove2(clicked, 'Panel' + parent_div)
        //alert($(clicked).parent().html())
        $(clicked).parent().remove()
        checkCentreColumn2();
      }
      else {
        var parent_div = 'RightColumn'
        $(clicked).addClass('REMOVEME')
        if ($(clicked).parent().parent().find('.StreamHolder').length == 1) {
          $(clicked).parent().parent().remove()
          initPager(parent_div)
          $('.' + parent_div + ' .StreamView:last').css({ display: 'block', left: '-350px' }).animate({ left: '14px' }, 300)
          //$('.Panel' + parent_div + ' .PagerPages span:last').click()
          $('.' + parent_div + ' .PagerPages .PageSelected').removeClass('PageSelected')
          $('.' + parent_div + ' .PagerPages span:last').addClass('PageSelected')
        }
        searchRemove2(clicked, parent_div)
        //alert($(clicked).parent().html())
        $(clicked).parent().remove()
        checkCentreColumn();
      }
    }
    else {
      $('#Panel3 .PanelInputFields .OxiValidation').html('Please unschedule before removing.').slideDown(300)
    }
  })
}


function autoLogIn(login_str) {
  var ajaxstuff = 'login,,' + login_str;
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    var login_success = 0;
    if (data != 0) {
      var login_success = 1
    }
    else {
      var login_success = 0
      $('#DivLoginBox div.OxiValidation:eq(0)').html('Login unsuccessful. Password or username may be incorrect.').slideDown(300)
      $('#ProgressLogin').css({ display: 'none' })
    }
    if (login_success == 1) {
      logged_in = 1
      $('#HeaderLoggedIn').css('display', 'inline').prev('span').css('display', 'none')
      //alert(data)
      $('#HeaderLoggedIn span').html(data)
      $('#HeaderLoggedOut').css('display', 'none')
      if ($('#ProgressLogin').attr('class') == 'SendMeToCreate') {
        location.href = '/CreateWizard.aspx'
      }
      else {
        unBlackOut2()
        dropDownAjax('DropDownPCs')
      }
    }
    endProgressBar('ProgressBackHolder');
  })
}

var logged_in;
function loginAjax() {
  $('#ProgressLogin').css({ display: 'block', opacity: '0.9' })
  var username = $('#DivLoginBox #LogEmailInput:visible input').val()
  var password = $('#DivLoginBox #LogPasswordInput:visible input').val()
  var remember = $('#DivLoginBox #LogRememberCheck:visible input[type="checkbox"]').is(':checked')
  if (remember == true) {
    //alert('222')
    toCookie('Login', (username + ',,' + password + ',,1'))
    remember = '1'
  }
  else {
    remember = '0'
  }
  var ajaxfile = ajax_path_put
  var ajaxstuff = 'login,,' + username + ',,' + password + ',,' + remember;
  $.post(ajaxfile, { command: ajaxstuff }, function(data) {
    var login_success = 0;
    if (data != 0) {
      var login_success = 1
    }
    else {
      var login_success = 0
      $('#DivLoginBox div.OxiValidation:eq(0)').html('Login unsuccessful. Password or username may be incorrect.').slideDown(300)
      $('#ProgressLogin').css({ display: 'none' })
    }
    if (login_success == 1) {
      $('#AddPCLink').css('visibility', 'visible')
      logged_in = 1
      $('#HeaderLoggedIn').css('display', 'inline').prev('span').css('display', 'none')
      $('#HeaderLoggedIn span').html(data)
      $('#HeaderLoggedIn2').css('display', 'inline').prev('span').css('display', 'inline')
      $('#HeaderLoggedIn2').next('span').css('display', 'inline')
      $('#HeaderLoggedOut').css('display', 'none')
      if ($('#ProgressLogin').attr('class') == 'SendMeToCreate') {
        location.href = '/CreateWizard.aspx'
      }
      else {
        unBlackOut2()
        dropDownAjax('DropDownPCs')
      }
    }
    endProgressBar('ProgressBackHolder');
  })
}

function logoutAjax() {
  var ajaxfile = ajax_path_put
  var timestamp = new Date().getTime()
  toCookie('Login', '-1')
  $.get(ajax_path_get + '?command=logout', function(data) {
    var logout_success = 0;
    if (data == 1) {
      var logout_success = 1
    }
    else {
      var logout_success = 0
    }
    if (logout_success == 1) {

      dropDownAjax('DropDownPCs')
      window.location = "Home.aspx"
    }
  })
}


var stream_icons_html = ''
function streamlistAjax(pc_id, container, folder_type, command_type) {
  initProgressBar('ProgressBackHolder')
  var ajax_array = new Array;
  if (folder_type == 1) {
    folder_type = 'pc'
    var remove_icon = '<div alt="Delete Content" title="Delete Content" onclick="removeContentAjax(this)" class="StreamRemove StreamIcon"></div>'
    var preview_icon = '<div onclick="popPreview(this)" alt="Preview" title="Preview" class="StreamPreview StreamIcon"></div>'
    stream_icons_html = remove_icon + preview_icon;
  }
  else {
    folder_type = 'folder'
    var remove_icon = '<div alt="Delete" title="Delete" onclick="removeContentAjax(this)" class="StreamRemove StreamIcon"></div>'
    var properties_icon = '<div alt="Edit Properties" title="Edit Properties" class="StreamProperties StreamIcon BindMeP">P</div>'
    var timings_icon = '<div onclick="popScheduleInfo(this);BlackOut2(\'DivPopUpTimings\')" alt="Slide Scheduling" title="Slide Scheduling" class="StreamTimings StreamIcon"></div>'
    var preview_icon = '<div onclick="popPreview(this)" alt="Preview" title="Preview" class="StreamPreview StreamIcon"></div>'
    stream_icons_html = remove_icon + properties_icon + timings_icon + preview_icon;
  }
  if (command_type == 4) {
    command_type = 'pcStreams'
  }
  else if (command_type == 0) {
    command_type = 'rawContent'
  }
  else if (command_type == 1) {
    command_type = 'streamSlides'
  }
  else {
    command_type = 'userStreams'
  }
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=' + command_type + '&' + folder_type + 'ID=' + pc_id + '&' + timestamp, function(data) {
    var ajax_str = data
    ajax_array = ajax_str.split('||')
    var final_array = new Array;
    for (var x = 0; x < ajax_array.length; x++) {
      final_array[x] = ajax_array[x].split(',,')
    }
    var col_class = $('#' + container)
    col_class.find('div.StreamView').remove()
    var stream_str = '<div class="StreamView">'
    if (command_type == 'userStreams') {
      stream_str += '<div onclick="thumbnailSelect(this)" class="StreamHolder StreamThumbnail"><div class="StreamImages"><img src="' + final_array[0][1] + '" alt=""/></div><span>Stream thumbnail</span></div>'
    }
    var stream_length = 0
    if (command_type == 'userStreams') {
      stream_length = 1
    }
    for (var z = 1; z < ajax_array.length; z++) {
      var stream_title = final_array[z][1]
      if (stream_title.length > 14 && (stream_title.indexOf(' ') > 14 || stream_title.indexOf(' ') == -1)) {
        stream_title = stream_title.substring(0, 14) + '<br/>' + stream_title.substring(14)
      }
      var stream_key = final_array[z][0]
      var stream_image = final_array[z][2]
      var weight_val = final_array[z][3]
      var private_val = final_array[z][4]
      var private_request = final_array[z][5]
      if (command_type == 'userStreams' && z == 1 && ajax_array.length == 1) {
        col_class.find('div.StreamView div.StreamThumbnail div.StreamImages img').attr('src', stream_image)
      }
      if (stream_length == 12) {
        stream_str += ('</div><div class="StreamView">')
        stream_length = 0;
      }
      stream_str += '<div onclick="chooseClickType(this);streamAjax(' + stream_key + ');return false" class="StreamHolder">' + stream_icons_html + '<div class="StreamImages"><img src="' + stream_image + '" alt="' + stream_title + '"/></div><span>' + stream_title + '</span><div class="StreamMeta"><span class="MetaKey">' + stream_key + '</span><span class="MetaWeight">' + weight_val + '</span><span class="MetaPrivate">' + private_val + '</span><span class="MetaRequest">' + private_request + '</span></div></div>'
      stream_length++;
      if (z == (ajax_array.length - 1)) {
        col_class.find('div.PagerNav').before(stream_str)
      }

    }
    if (window.location.href.indexOf('CreateWizard.aspx') != -1) {
      col_class.find('.StreamView:first').css({ left: '16px' })
    }
    else {
      col_class.find('.StreamView:first').css({ left: '2px' })
    }
    if (command_type == 'rawContent' && $('#DivPopUpUploader').is(':visible')) {
      unUploaderBlackOut()
    }
    if (folder_type == 'pc') {
      initPrivacy('LeftColumn')
      initPrivacy('RightColumn')
      initPager('LeftColumn')
      addPreview()
      addCrosses2()
      streamBrowseView();
      showIcons();
    }
    else {
      if (command_type == 'streamSlides') {
        setTimeout("initPager('Panel4');streamBrowseView2();updatePager();showIcons();", 500)
      }
      initPager('Panel2');
      initPager('Panel3');
      initPager('Panel4');
      updatePager();
      streamBrowseView2();
      addCrosses2()
      addProperties('Panel2');
      addProperties('Panel3');
      addTimings()
      addPreview()
      showIcons();
    }
    addCrosses(); addWeight();
    if (isShowWeight() == '1') {
      showWeighting()
    }
    if (command_type == 'rawContent' && whatpanel == 1) {
      updateArrowNav(1)
    }
    if (command_type == 'userStreams' && whatpanel == 2) {
      updateArrowNav(2)
    }
    if (command_type == 'streamSlides' && whatpanel == 2) {
      updateArrowNav(2)
    }
    if (content_pager_pos > 0) {
      $('#Panel2 div.StreamView').stop(true, true).css({ left: '100px', display: 'none' })
      $('#Panel2 div.StreamView:eq(' + content_pager_pos + ')').css({ left: '16px', display: 'block' })
      $('#Panel2 .PagerPages span:eq(' + content_pager_pos + ')').click()
      $('#Panel2 .PagerPages .PageSelected').removeClass('PageSelected')
      $('#Panel2 .PagerPages span:eq(' + content_pager_pos + ')').addClass('PageSelected')
    }
    if (content_pager_pos_last > 0) {
      $('#Panel2 div.StreamView').stop(true, true).css({ left: '100px', display: 'none' })
      $('#Panel2 div.StreamView:last').css({ left: '16px', display: 'block' })
      $('#Panel2 .PagerPages span:last').click()
      $('#Panel2 .PagerPages .PageSelected').removeClass('PageSelected')
      $('#Panel2 .PagerPages span:last').addClass('PageSelected')
      content_pager_pos_last--
    }
    if (slide_pager_pos > 0) {
      $('#Panel3 div.StreamView').stop(true, true).css({ left: '100px', display: 'none' })
      $('#Panel3 div.StreamView:eq(' + slide_pager_pos + ')').css({ left: '16px', display: 'block' })
      $('#Panel3 .PagerPages span:eq(' + slide_pager_pos + ')').click()
      $('#Panel3 .PagerPages .PageSelected').removeClass('PageSelected')
      $('#Panel3 .PagerPages span:eq(' + slide_pager_pos + ')').addClass('PageSelected')
    }
    if (stream_pager_pos > 0) {
      //alert('222')
      $('#Panel4 div.StreamView').stop(true, true).css({ left: '100px', display: 'none' })
      $('#Panel4 div.StreamView:eq(' + stream_pager_pos + ')').css({ left: '16px', display: 'block' })
      $('#Panel4 .PagerPages span:eq(' + stream_pager_pos + ')').click()
      $('#Panel4 .PagerPages .PageSelected').removeClass('PageSelected')
      $('#Panel4 .PagerPages span:eq(' + stream_pager_pos + ')').addClass('PageSelected')
    }
    endProgressBar('ProgressBackHolder');
    if (container == 'RightColumn') {
      initPager('RightColumn')
    }
  })
}

var available_thumbs;
function thumbnailSelect(thumbnail) {
  available_thumbs = $(thumbnail).parent().parent().find('div.StreamHolder:not(.StreamThumbnail)')
  $(thumbnail).css('backgroundImage', 'url(/images/default/stream-green-blink.gif)').find('img').attr('src', '/images/default/thumbnail-select.gif')
  available_thumbs.unbind('click', streamClick2)
  available_thumbs.bind('click', thumbnailSelectInner)
}
function thumbnailSelectInner() {
  $('.StreamThumbnail .StreamImages img').attr('src', $(this).find('img').attr('src'))
  $('.StreamThumbnail').css('backgroundImage', 'url(/images/default/stream-green.gif)')
  $(this).closest('div.StreamView').parent().find('div.StreamView:first').addClass('FirstStream')
  if ($(this).closest('div.StreamView').is(':not(.FirstStream)')) {
    $(this).closest('div.StreamView').animate({ left: '350px' }, 300, function() {
      $(this).css('display', 'none')
    })
    $(this).closest('div.StreamView').parent().find('div.StreamView:first').css({ display: 'block', left: '-350px' }).animate({ left: '16px' }, 300)
  }
  var stream_id = $('#ForJQStreamDropDown option:selected').val()
  var ajaxstuff = 'putStreamThumbnail,,' + stream_id + ',,' + $(this).find('img').attr('src')
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
  })
  available_thumbs.bind('click', streamClick2)
  available_thumbs.unbind('click', thumbnailSelectInner)
}

function dropDownAjax(dropdown) {
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=pcs&' + timestamp, function(data) {
    var ajax_str = data
    var ajax_error = 0;
    if (ajax_str.length == 0) {
      ajax_error = 1
    }
    ajax_array = ajax_str.split('||')
    var final_array = new Array;
    for (var x = 0; x < ajax_array.length; x++) {
      final_array[x] = ajax_array[x].split(',,')
    }
    $('#' + dropdown + ' select').empty()
    if (ajax_error == 0) {
      for (var r = 0; r < ajax_array.length; r++) {
        var pc_name = final_array[r][1]
        var pc_id = final_array[r][0]
        var pc_phantom = final_array[r][2]
        var container = '\'RightColumn\''
        $('#' + dropdown + ' select').append('<option value="' + pc_id + '">' + pc_name + '</option>')
        $('#' + dropdown + ' select option:last').data('phantom', pc_phantom)
      }
      var initial_option = $('#' + dropdown + ' select option:first').attr('value')
      streamlistAjax(initial_option, 'RightColumn', '1', '4')
    }
    else {
      var pc_id = '-1'
      $('#' + dropdown + ' select').append('<option value="' + pc_id + '" onfocus="streamlistAjax(' + pc_id + ',' + container + ', 1)">My PC</option>')
    }
  })
}

function popUploaderDropdown(imageUploaderClientID) {
  $('#UploadUserFolder').empty().html($('#DropDownFolders select').html())
  $('#UploadUserFolder option:eq(' + $('#DropDownFolders select option').index($('#DropDownFolders select option:selected')) + ')').attr('selected', 'selected')
}

//  function setMaxTotalSize(img_uploader, tmp_size) {
//    tmp_size = parseInt(ajax_array[1]) * 1.1;
//    try {
//      getImageUploader(img_uploader).setMaxTotalFileSize(tmp_size)
//    }
//    catch (err) {
//      alert(err.description)
//      //setMaxTotalSize()
//    }
//  }

function bytesConverter(bytes) {
  var gigabyte = 1073741824;
  var megabyte = 1048576;
  var kilobyte = 1024;
  if (bytes > gigabyte) {
    bytes = roundNumber((bytes / gigabyte), 2) + ' gb'
  }
  else if (bytes > megabyte) {
    bytes = roundNumber((bytes / megabyte), 2) + ' mb'
  }
  else if (bytes > kilobyte) {
    bytes = roundNumber((bytes / kilobyte), 2) + ' kb'
  }
  else {
    bytes = bytes + 'b'
  }
  return bytes;
}

function roundNumber(num, dec) {
  var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
  return result;
}

var container_array = new Array('Panel2', 'Panel3', 'Panel4')
function createDropDownAjax(which_one,memory) {
  var pick_drop;
  if (which_one) {
    pick_drop = which_one
  }
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=folders&' + timestamp, function(data) {
    var dropdown_array = new Array('DropDownFolders', 'ForJQSlideDropDown', 'ForJQStreamDropDown')
    var ajax_str = data
    var ajax_error = 0;
    if (ajax_str.indexOf('|') == -1) {
      ajax_error = 1
    }
    ajax_array = ajax_str.split('||')
    var final_array = new Array;
    for (var x = 0; x < ajax_array.length; x++) {
      final_array[x] = ajax_array[x].split(',,')
    }
    if (ajax_error == 0) {
      var loop_start = 0;
      var loop_end = ajax_array.length;
      if (pick_drop) {
        $('#' + dropdown_array[pick_drop] + ' select').empty()
        loop_start = pick_drop;
        loop_end = pick_drop + 1;
      }
      else {
        $('#' + dropdown_array[0] + ' select').empty()
        $('#' + dropdown_array[1] + ' select').empty()
        $('#' + dropdown_array[2] + ' select').empty()
      }
      for (var h = loop_start; h < loop_end; h++) {
        var dropdown_html = ''
        for (var r = 0; r < final_array[h].length; r = r + 2) {
          dropdown_html += '<option value="' + final_array[h][r] + '">' + final_array[h][r + 1] + '</option>'
        }
        $('#' + dropdown_array[h] + ' select').html(dropdown_html)
        if (h == 0) {
          $('#' + dropdown_array[h] + ' select option:eq(' + content_dropdown_pos + ')').attr('selected', 'selected')
        }
        if (h == 1) {
          $('#' + dropdown_array[h] + ' select option:eq(' + slide_dropdown_pos + ')').attr('selected', 'selected')
        }
        if (h == 2) {
          $('#' + dropdown_array[h] + ' select option:eq(' + stream_dropdown_pos + ')').attr('selected', 'selected')
        }
        $('#' + dropdown_array[h] + ' select').change(function() {
          dropDownStreamPopulate(this)
        });
        if (memory) {
          dropDownStreamPopulate($('#' + dropdown_array[h] + ' select'), memory)
        }
        else {
          dropDownStreamPopulate($('#' + dropdown_array[h] + ' select'))
        }
      }
    }
    else {
      var pc_id = '-1'
      $('#' + dropdown_array[0] + ' select').append('<option>My Folder</option>')
      $('#' + dropdown_array[1] + ' select').append('<option>My Folder</option>')
      $('#' + dropdown_array[2] + ' select').append('<option>My Folder</option>')
    }
    updateArrowNav(whatpanel)
  })
  //$('#ProgressBackHolder').css('display', 'none')

}

function dropDownStreamPopulate(clicked,memory) {
  var clicked_panel = $(clicked).closest('div.Panel')
  if (memory != null) {
    //Don't reset memory
  }
  else {
    if (clicked_panel.is('#Panel2')) {
      $('#Panel2 div.PagerPages span.PageSelected').removeClass('PageSelected')
      $('#Panel2 div.PagerPages span:first').addClass('PageSelected')
      content_pager_pos = 0;
    }
    else if (clicked_panel.is('#Panel3')) {
      $('#Panel3 div.PagerPages span.PageSelected').removeClass('PageSelected')
      $('#Panel3 div.PagerPages span:first').addClass('PageSelected')
      slide_pager_pos = 0;
    }
    else {
      $('#Panel4 div.PagerPages span.PageSelected').removeClass('PageSelected')
      $('#Panel4 div.PagerPages span:first').addClass('PageSelected')
      stream_pager_pos = 0;
    }
  }
  createMemory();
  //TODO: CH HACK - Ryan to fix properly
  if ($(clicked).find('option:selected'))
    var selected_folder = $(clicked).find('option:selected').attr('value')
  //TODO: CH HACK Ryan to fix properly
  if ($(clicked).attr('class'))
    var selected_dropdown = $(clicked).attr('class').charAt(27) - 1
    streamlistAjax(selected_folder, container_array[selected_dropdown], 0, selected_dropdown)
}

function searchAjax(clicked) {
  var ajax_array = new Array;
  var search_str = $(clicked).closest('div.SearchPanel').find('input').val()
  var hidden_field = $('#ForJSDownloadHidden input').val()
  if (hidden_field.length > 0) {
    $('.MiniNavDivs:eq(1) input').removeAttr('value')
    $('#ForJSDownloadHidden input').removeAttr('value')
  }
  var fixed_search_str = escape(search_str)
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=search&keyword=' + fixed_search_str + '&' + timestamp, function(data) {
    var ajax_str = data
    if (data.length != 0) {

      ajax_array = ajax_str.split('||')
      var final_array = new Array;

      for (var x = 0; x < ajax_array.length; x++) {
        final_array[x] = ajax_array[x].split(',,')
      }
      if ($('div.MiniNavigation .Active a').html() == 'Browse') {
        var col_class = $('div.LeftColumn div.MiniNavDivs:eq(0)')
        col_class.find('div.StreamView').remove()
      }
      else {
        var col_class = $('div.LeftColumn div.MiniNavDivs:eq(1)')
        col_class.find('div.StreamView').remove()
      }
      var stream_length = 0;
      var stream_str = '<div class="StreamView">'
      for (var z = 0; z < ajax_array.length; z++) {
        var stream_title = final_array[z][1]
        var stream_key = final_array[z][0]
        var stream_image = final_array[z][2]
        var private_stream = final_array[z][3]
        var stream_accept = final_array[z][4]
        if (private_stream == 'true') {
          private_stream = 1;
        }
        if (stream_length == 12) {
          stream_str += ('</div><div class="StreamView">')
          stream_length = 0;
        }
        stream_str += '<div onclick="streamAjax(' + stream_key + ');chooseClickType(this);return false" class="StreamHolder">' + stream_icons_html + '<div class="StreamImages"><img src="' + stream_image + '" alt="' + stream_title + '"/></div><span>' + stream_title + '</span><div class="StreamMeta"><span class="MetaKey">' + stream_key + '</span><span class="MetaWeight">10</span><span class="MetaPrivate">' + private_stream + '</span><span class="MetaRequest">' + stream_accept + '</span></div></div>'
        stream_length++;
        if (z == (ajax_array.length - 1)) {
          stream_str += '</div>'
          col_class.find('div.PagerNav').before(stream_str)
        }
        //          if (stream_length == 12) {
        //            col_class.find('.StreamView:last').after('<div class="StreamView"></div>')
        //          }
        //          col_class.find('.StreamView:last').append('<div onclick="streamAjax(' + stream_key + ');return false" class="StreamHolder"><div class="StreamImages"><img src="' + stream_image + '" alt="' + stream_title + '"/></div><span>' + stream_title + '</span><div class="StreamMeta"><span class="MetaKey">' + stream_key + '</span><span class="MetaWeight">10</span><span class="MetaPrivate">' + private_stream + '</span></div></div>')
      }
    }
    else {
      var col_class = $('div.LeftColumn div.MiniNavDivs:eq(1)')
      col_class.find('div.StreamView').remove()
      col_class.find('div.SearchPanel').after('<div class="StreamView"></div>')
      col_class.find('.StreamView').append(error_pre + 'Sorry no results were found for those search terms' + error_app)
    }
    initPrivacy('LeftColumn')
    initPrivacy('RightColumn')
    initPager('LeftColumn')
    streamBrowseView();



  })
}

var error_pre = '<div class="ErrorDiv"><div class="ErrorTop"></div><div class="ErrorMiddle"><p>'
var error_app = '</p></div><div class="ErrorBottom"></div></div>'

function ajaxSortBy(clicked) {
  $(clicked).parent().find('.Active').removeClass('Active').addClass('Inactive')
  $(clicked).addClass('Active').removeClass('Inactive')
  listAjax('Sorting')
}

var current_category;
function listAjax(cat_id) {
  if (cat_id == 'Sorting') {
    cat_id = $('#BrowseNav .NavHolder:last a:first').attr('id')
  }
  var sorted;
  var sort_by = $('#SortBy a').index($('#SortBy .Active'))
  if (sort_by == 0) {
    sorted = 'p'
  }
  if (sort_by == 1) {
    sorted = 'r'
  }
  if (sort_by == 2) {
    sorted = 'a'
  }
  current_category = cat_id
  var ajax_array = new Array;
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=channels&categoryid=' + cat_id + '&startpageno=1&endpageno=100&sortby=' + sorted + '&' + timestamp, function(data) {

    var ajax_str = data


    if (data.length != 0 && data.indexOf('Error: The server was unable to process the request due to an internal error.') == -1) {
      ajax_array = ajax_str.split('||')
      var final_array = new Array;
      for (var x = 0; x < ajax_array.length; x++) {
        final_array[x] = ajax_array[x].split(',,')
      }
      if ($('div.MiniNavigation .Active a').html() == 'Browse' || cat_id == -1) {
        var col_class = $('div.LeftColumn div.MiniNavDivs:eq(0)')
        col_class.find('div.StreamView').remove()
      }
      else {
        var col_class = $('div.LeftColumn div.MiniNavDivs:eq(1)')
        col_class.find('div.StreamView').remove()
      }
      var stream_length = 0;
      var stream_str = '<div class="StreamView">'
      for (var z = 1; z < ajax_array.length; z++) {
        var stream_title = final_array[z][1]
        var stream_key = final_array[z][0]
        var stream_image = final_array[z][2]
        var private_stream = final_array[z][3]
        var private_request = '';
        if (final_array[z][4]) {
          if (final_array[z][4] == '1') {
            private_request = final_array[z][4];
          }
        }
        if (private_stream == 'true') {
          private_stream = 1;
        }
        if (stream_length == 12) {
          stream_str += ('</div><div class="StreamView">')
          stream_length = 0;
        }
        stream_str += '<div onclick="streamAjax(' + stream_key + ');chooseClickType(this);return false" class="StreamHolder">' + stream_icons_html + '<div class="StreamImages"><img src="' + stream_image + '" alt="' + stream_title + '"/></div><span>' + stream_title + '</span><div class="StreamMeta"><span class="MetaKey">' + stream_key + '</span><span class="MetaWeight">10</span><span class="MetaPrivate">' + private_stream + '</span><span class="MetaRequest">' + private_request + '</span></div></div>'
        stream_length++;
        if (z == (ajax_array.length - 1)) {
          col_class.find('div.PagerNav').before(stream_str)
        }
      }
    }
    else if (data.length == 0) {
      var col_class = $('div.LeftColumn div.MiniNavDivs:eq(0)')
      col_class.find('div.StreamView').remove()
      col_class.find('div.SortBy').after('<div class="StreamView"></div>')
      col_class.find('.StreamView').append(error_pre + 'Sorry there are no streams currently in this category' + error_app)
    }
    else {
      var col_class = $('div.LeftColumn div.MiniNavDivs:eq(0)')
      col_class.find('div.StreamView').remove()
      col_class.find('div.SortBy').after('<div class="StreamView"></div>')
      col_class.find('.StreamView').append(error_pre + 'An error has occurred' + error_app)
    }
    initPrivacy('LeftColumn')
    initPrivacy('RightColumn')
    initPager('LeftColumn')
    streamBrowseView();

  })
}

function streamAjax(stream_id) {
  var ajax_array = new Array;
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=channeldetails&channelid=' + stream_id + '&' + timestamp, function(data) {
    if ($('#LeftColumn .SelectedStream').length) {
      var ajax_str = data
      ajax_array = ajax_str.split('||')
      var final_array = new Array;
      for (var t = 0; t < ajax_array.length; t++) {
        final_array[t] = ajax_array[t].split(',,')
      }
      for (var h = 0; h < ajax_array.length; h++) {
        var stream_about = final_array[0][0]
        var stream_date = final_array[0][1]
        var stream_by = final_array[0][2]
        var stream_content = final_array[0][3]
        var stream_follow = final_array[0][4]
        var stream_details = final_array[0][5]
        var parent_box = $('#CentreColumn div.CentreMeta div.MetaDiv')
        parent_box.find('#MetaAbout').html(stream_about)
        parent_box.find('#MetaDateAdded').html(stream_date)
        parent_box.find('#MetaAddedBy').html(stream_by)
        parent_box.find('#MetaContent').html(stream_content)
        parent_box.find('#MetaFollowers').html(stream_follow)
        parent_box.find('#MetaLink').attr('href', 'ChannelDetails.aspx?channelID=' + stream_id)
      }
    }
  })
}

function browseAjax(cat_id) {
  var ajax_array = new Array;
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=categories&categoryid=' + cat_id + '&' + timestamp, function(data) {
    //alert(data)
    var ajax_str = data
    ajax_array = ajax_str.split('||')
    var final_array = new Array;
    for (var i = 0; i < ajax_array.length; i++) {
      final_array[i] = ajax_array[i].split(',,')
    }
    if (window.location.href.indexOf('Download.aspx') != -1) {
      var current_browse = $('div.BrowseDrop:last')
    }
    else {
      var current_browse = $('#DivPopUpStreamProperties:first div.BrowseDrop:last')
    }
    current_browse.find('a').remove()
    for (var j = 0; j < ajax_array.length; j++) {
      current_browse.append('<a onclick="browseDropClick(this);listAjax(' + final_array[j][0] + ');return false" id="' + final_array[j][0] + '" href="#">' + final_array[j][1] + '</a>')
    }
  })
}

var nav_speed = 400;
var drop_speed = 300;

function bindBrowseHover() {
  //alert('222')
  //alert($(this).find('a').attr('id'))
  $('.BrowseDrop').stop(true, true).css('display', 'none')
  if ($('.BrowseDrop').length == 1 || $('#DivPopUpStreamProperties:first .BrowseDrop').length == 1) {
    browseAjax($(this).find('.BrowseMain').attr('id'))
  }
  $('.BrowseNav *:animated').stop(true, true)
  //alert($(this).find('.BrowseDrop a:first').html())
  if ($(this).find('.BrowseDrop').is(':hidden') && $(this).find('.BrowseDrop a:first').html() != 'undefined') {
    if ($(this).is('.ClosedNav')) {
      $('.BrowseNav div').stop(true, true)

      $(this).parent().find('.OpenNav:last').find('.BrowseMain').animate({ width: '0px', paddingLeft: '31px' }, nav_speed)
      $(this).parent().find('.OpenNav:last').removeClass('OpenNav').addClass('ClosedNav').animate({ width: '31px' }, nav_speed, function() {
        $(this).find('img').attr('src', 'Images/Default/browse-nav-closed.png').css('width', '31px')
      })

      $(this).find('.BrowseMain').animate({ width: '96px', paddingLeft: '15px' }, nav_speed)
      $(this).find('img').attr('src', 'Images/Default/browse-nav-open.png').css('width', '111px')
      $(this).removeClass('ClosedNav').addClass('OpenNav').animate({ width: '111px' }, nav_speed, function() {
        $(this).parent().find('.BrowseDrop').slideUp(drop_speed)
        $(this).find('.BrowseDrop').slideDown(drop_speed)
      })
    }
    else if ($(this).is('.OpenNav')) {
      $(this).parent().find('.BrowseDrop').slideUp(drop_speed)
      $(this).find('.BrowseDrop').slideDown(drop_speed)
    }
    $('.OpenNav img').attr('src', 'Images/Default/browse-nav-open.png')
    $('.OpenNav img').css('width', '111px')
  }
}

function bindBrowseHoverOut() {
  $(this).find('.BrowseDrop').stop(true, true).slideUp(drop_speed)//, function() { 
}

var temp_var = 0;
function browseDropClick(clicked) {
  $('.BrowseDrop').css('display', 'none')
  if ($(clicked).parent().parent().nextAll('.NavHolder').length >= 1) {
    temp_var = 1;
    $(clicked).parent().parent().parent().find('.NavHolder:last')
    refreshBrowse();
    $(clicked).parent().parent().nextAll('.NavHolder').css({ display: 'none' }).remove()
  }
  if (window.location.href.indexOf('Download.aspx') != -1) {
    var browse_fix = $('.BrowseNav .NavHolder')
  }
  else {
    var browse_fix = $('#DivPopUpStreamProperties:first .BrowseNav .NavHolder')
  }
  if (browse_fix.length >= 3) {
    if ($(clicked).parent().parent().next('.NavHolder').length == 1) {
      var is_closed = 0
    }
    else {
      var is_closed = 1
    }
  }
  else {
    var is_closed = 0
  }
  var next_cat = $(clicked).html()
  var curr_cat = $(clicked).parent().parent().find('.BrowseMain span').html()
  var next_cat_id = $(clicked).attr('id')
  var new_cat = '<div class="NavHolder OpenNav NewCat"><a onclick="killNav(this)" class="BrowseMain" href="#" id="' + next_cat_id + '"><img src="Images/Default/browse-nav-closed.png" alt="" /><span>' + next_cat + '</span></a><div class="BrowseDrop"><div class="DropBottom"></div></div><div class="BrowseParent">' + curr_cat + '</div></div>'
  $('#BrowseNav').append(new_cat)
  browseAjax(next_cat_id)
  var new_cat2 = $('#BrowseNav .NewCat')
  initBrowseHoverEffects();
  new_cat2.css('width', '31px')
  new_cat2.find('.BrowseMain').css({ width: '0px', paddingLeft: '31px' })
  new_cat2.find('img').css({ width: '31px' })
  new_cat2.find('.BrowseMain').animate({ width: '96px', paddingLeft: '15px' }, nav_speed)
  new_cat2.find('img').attr('src', 'Images/Default/browse-nav-open.png').css('width', '111px')
  new_cat2.removeClass('ClosedNav').addClass('OpenNav').animate({ width: '111px' }, nav_speed)
  if (is_closed == 1) {
    if (window.location.href.indexOf('Download.aspx') != -1) {
      var quick_fix = $('#BrowseNav')
    }
    else {
      var quick_fix = $('#DivPopUpStreamProperties:first #BrowseNav')
    }
    if (quick_fix.find('.NavHolder').length == 7) {
      quick_fix.find('.NavHolder:eq(0)').remove()
    }
    quick_fix.find('.OpenNav:first').find('.BrowseMain').animate({ width: '0px', paddingLeft: '31px' }, nav_speed)
    quick_fix.find('.OpenNav:first').removeClass('OpenNav').addClass('ClosedNav').animate({ width: '31px' }, nav_speed, function() {
      $(this).find('img').attr('src', 'Images/Default/browse-nav-closed.png').css('width', '31px')
    })
  }
  initBrowseNav()
  new_cat2.removeClass('NewCat')

  if (window.location.href.indexOf('CreateWizard.aspx') != -1) {
    $('#BrowseTree').next('div').css('display', 'block')
    var browse_tree = ''
    var browse_var = 0
    $('#BrowseNav .BrowseMain').each(function() {
    browse_tree = browse_tree + '<span id="' + $(this).attr('id') + '">&nbsp;' + $(this).find('span').html() + '&nbsp;</span>'
      browse_var++
    });
    $('#BrowseTree').html(browse_tree)
    $('#BrowseTree span:first').remove()
    if ($('#BrowseTree span').length > 1) {
    }
    $('#BrowseTree span:not(#BrowseTree span:first)').prepend('&gt;')
    $('#BrowseTree').prepend('<span>Your Stream will be in:</span>')
    $('#BrowseTree').css('display', 'block')
  }

  return false;
}

var new_parent = 'All Categories' //TEMPORARY
function refreshBrowse() {
  var possible_parent = $('.BrowseNav .NavHolder:eq(0) .BrowseParent').html()
  var next_cat = possible_parent;
  var new_cat = '<div class="NavHolder NewCat OpenNav"><a class="BrowseMain" href="#"><img src="Images/Default/browse-nav-closed.png" alt="" /><span>' + next_cat + '</span></a><div class="BrowseDrop"><div class="DropBottom"></div></div><div class="BrowseParent">' + new_parent + '</div></div>'
  if (possible_parent.length > 1 && $('.BrowseNav .NavHolder').length < 7) {
    $('.BrowseNav').prepend(new_cat)
    new_parent = ''//TEMPORARY
    initBrowseHoverEffects()
    $('.NewCat').removeClass('NewCat')
    refreshBrowse()
  }
  else {

  }
}

function killNav(clicked) {
  if ($(clicked).parent().next('div.NavHolder').length > 0) {
    $(clicked).parent().nextAll('.NavHolder').remove()
    listAjax($(clicked).attr('id'))
    //      refreshBrowse();
    //      initBrowseNav();
    if ($(clicked).parent().parent().find('div.NavHolder').length == 7) {
      $(clicked).parent().parent().find('div.NavHolder:first').remove()
    }
  }
  return false;
}

function saveCategory() {
  var cat_id = $('#BrowseTree span:last').attr('id')
  var stream_id = $('#ForJQStreamDropDown option:selected').attr('value')
  var ajaxstuff = 'setCategory,,' + stream_id + ',,' + cat_id
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {

  })
}

function testProgress() {
  //$('.' + panel + ' div').css('opacity', '0.5')
  var v_pos = (parseInt($('.MainPanel').height()) / 2)
  var h_pos = (parseInt($('.MainPanel').width()) / 2) - 11
  //alert(v_pos)
  //if (location.href.indexOf('CreateWizard.aspx') != -1) {
  //v_pos = v_pos - 70
  //}
  var main_panel = $('div.MainPanel')
  main_panel.append('<div class="ProgressInd" id="Progress"></div>')
  main_panel.append('<div id="ProgressBackHolder"></div>')
  var fade_cover = main_panel.find('div#ProgressBackHolder')
  var prog_bar = main_panel.find('div#Progress')
  fade_cover.append('<img id="ProgressCoverTop" src="../Images/Default/panel-cover-top.png" />')
  fade_cover.append('<img id="ProgressCover" src="../Images/Default/panel-cover-middle.png" />')
  fade_cover.append('<img id="ProgressCoverBot" src="../Images/Default/panel-cover-bot.png" />')
  fade_cover.append('<div class="ProgressInd"></div>')

  fade_cover.fadeIn(400, function() {
    prog_bar.css({ display: 'block', top: v_pos + 'px', left: h_pos + 'px' })
    //TEMPORARY
    prog_bar.animate({ left: h_pos + 'px' }, 5000, function() {
      prog_bar.remove()
      fade_cover.fadeOut(400, function() {
        $(this).remove()
      })
    })

  })
}

var progress_var = 0
function initProgressBar(prog_bar) {
  var fade_cover = $('div#' + prog_bar)
  if (progress_var == 0) {
    $('div#' + prog_bar).css({ display: 'block'})
    $('div.CreateArrowLeft,div.CreateArrowRight').css('opacity', '0.2')
  }
  progress_var++
}
function endProgressBar(prog_bar) {
  progress_var--
  //alert(progress_var)
  if (progress_var <= 0) {
    $('div#' + prog_bar).css({ display: 'none' })
    $('div.CreateArrowLeft,div.CreateArrowRight').css('opacity', '1')
    progress_bar = 0;
  }
}

function initBrowseHoverEffects() {
  var target = $('#BrowseNav div.NavHolder')
  target.unbind('mouseenter', bindBrowseHover)
  target.unbind('mouseleave', bindBrowseHoverOut)
  target.bind('mouseenter', bindBrowseHover)
  target.bind('mouseleave', bindBrowseHoverOut)
}

function isShowWeight() {
  if ($('span.WeightingLink a:eq(0)').is('.WeightingSpanSelected')) {
    var show_weight = 1
  }
  return show_weight
}

function showWeighting() {
  $('div.StreamWeight:not(#CentreColumn .StreamWeight)').stop(true, true).fadeIn(200)
  $('span.WeightingLink a:eq(1)').removeClass('WeightingSpanSelected')
  $('span.WeightingLink a:eq(0)').addClass('WeightingSpanSelected')

  $('div.StreamWeight').each(function() {
    if ($(this).html() == 100) {
      $(this).css({ fontSize: '1em', paddingTop: '3px', height: '18px', paddingLeft: '2px', width: '20px' })
    }
    else if ($(this).html() < 10) {
      $(this).css({ fontSize: '1.4em', paddingTop: '1px', height: '20px', paddingLeft: '8px', width: '14px' })
    }
    else {
      $(this).css({ fontSize: '1.4em', paddingTop: '1px', height: '20px', paddingLeft: '3px', width: '19px' })
    }
  });

}
function hideWeighting() {
  $('div.StreamWeight').stop(true, true).fadeOut(200)
  $('span.WeightingLink a:eq(0)').removeClass('WeightingSpanSelected')
  $('span.WeightingLink a:eq(1)').addClass('WeightingSpanSelected')
}

function setAlert() {
  $(this).closest('div.DivPopUp').find('span.IsChanged').html('1')
}
function alertDiscard() {
  $('div.DivPopUpAlert').slideUp(200, function() {
    $('div.DivPopUp:visible').css('zIndex', '10000')
    if ($('#DivPopUpTimings').is(':visible')) {
      unBlackOut3($('#DivPopUpTimings div:eq(0)'))
    }
    else if ($('#DivPopUpContentProperties').is(':visible')) {
      $('#DivPopUpContentProperties').slideUp(300)
      $('#CoverLayer').fadeOut(200)
    }
    else if ($('#DivPopUpStreamProperties').is(':visible')) {
    $('#DivPopUpStreamProperties').slideUp(300)
      $('#CoverLayer').fadeOut(200)
    }
    else {
      unBlackOut2()
    }
  });
}
function alertNoDiscard() {
  $('div.DivPopUpAlert').slideUp(200, function() {
    $('div.DivPopUp:visible').css('zIndex', '10000')
  });
}
function alertDiscard2() {
  $('div.DivPopUpAlert2').slideUp(200, function() {
    $('div.DivPopUp:visible').css('zIndex', '10000')

  });
}
function alertNoDiscard2() {
  $('div.DivPopUpAlert2').slideUp(200, function() {
    $('div.DivPopUp:visible').css('zIndex', '10000')
  });
}

function dropDownShow() {
  var property_hidden = $(this).closest('div.RFormBox').next().next('div.PropertyHidden')
  var selected_value = $(this).html()
  var selected_index = $(this).parent().find('option').index(this)
  if (selected_index != 0) {
    property_hidden.find('input:first').attr('value', selected_value)
    property_hidden.slideDown(400, function() {
      property_hidden.find('input:first').focus()
    })
  }
  else {
    property_hidden.slideUp(400)
  }
}

function textBoxSpecial() {
  var default_value = $(this).prev('span.TextBoxSpecialInfo').html()
  if ($(this).val() == default_value) {
    $(this).removeAttr('value')
  }
  $(this).blur(function() {
    if ($(this).val().length < 1) {
      $(this).attr('value', default_value)
    }
  });
}

function transferOptions(from_id, to_id, default_state) {
  var destination;
  if ($('#' + to_id).is('select')) {
    destination = $('#' + to_id)
  }
  else {
    destination = $('#' + to_id).find('select')
  }
  if (default_state == 1) {
    destination.find('option:gt(0)').remove()
  }
  else {
    destination.find('option').remove()
  }
  if (default_state == 2) {
    $('#' + from_id).find('option:gt(0)').each(function() {
      destination.append('<option>' + $(this).html() + '</option>')
    })
  }
  else {
    $('#' + from_id).find('option').each(function() {
      destination.append('<option>' + $(this).html() + '</option>')
    })
  }
  if (destination.is('.DropDownShow') == true) {
    $('select.DropDownShow option').unbind('click', dropDownShow)
    $('select.DropDownShow option').bind('click', dropDownShow)
  }
}

function addNewOption(from_id, new_content) {
  $('#' + from_id + ' select').append('<option>' + new_content + '</option>')
}

function addOption(from_id, to_id) {
  $('#' + to_id + ':visible').append('<option>' + $('#' + from_id + ':visible').val() + '</option>')
  $('select.DropDownShow option').unbind('click', dropDownShow)
  $('select.DropDownShow option').bind('click', dropDownShow)
}

function replaceOption(from_id, to_id) {
  $('#' + to_id + ':visible option:selected').html($('#' + from_id + ':visible').val())
  //    $('select.DropDownShow option').unbind('click', dropDownShow)
  //    $('select.DropDownShow option').bind('click', dropDownShow)
}

function helpActivate(clicked) {
  var contents = '';
  $(clicked).find('p').each(function() {
    contents = contents + $(this).html() + '<br/><br/>'
  });
  if (contents.length < 500) {
    $('#HelpPopUp').addClass('DivPopUpSmall')
  }
  else {
    $('#HelpPopUp').removeClass('DivPopUpSmall')
  }
  $('#HelpPopUp div.DivPopMiddle p').html(contents)
  BlackOut('HelpPopUp')
}

function windowsFolder() {
  var active_folder = 'FolderOpen'
  var windows_view = $('div.WindowsView:visible')
  var windows_edit = windows_view.find('div.RFormBox input')
  var windows_tree = windows_view.find('div.WindowsTree .WindowsTreeContent')
  $(this).parent().find('.' + active_folder).removeClass(active_folder)
  $(this).addClass(active_folder)
  $('#ContentFolderName').html('in ' + $(this).html())
  windows_edit.val(windows_tree.find('div.FolderOpen').html())
  var this_index = $(this).parent().find('.WindowsFolder').index(this)
  var window_window = $(this).closest('div.WindowsView').find('div.WindowsHolder:eq(' + this_index + ')')
  $(this).closest('div.WindowsView').find('div.WindowsHolder').css('display', 'none')
  window_window.css('display', 'block')
}

function createWindows() {
  var windows_view = $('div.WindowsView')
  var windows_tree = windows_view.find('div.WindowsTree .WindowsTreeContent')
  var windows_folders = windows_tree.find('div.WindowsFolder')
  windows_folders.unbind('click', windowsFolder)
  windows_folders.bind('click', windowsFolder)
  windows_folders.bind('dblclick', windowsRename)

  //$('.WindowsFileDepo').unbind('mousedown', windowsSelectDrag)
  //$('.WindowsFileDepo').bind('mousedown', windowsSelectDrag)
}

function windowsRename() {
  var windows_view = $('div.WindowsView')
  var windows_tree = windows_view.find('div.WindowsTree .WindowsTreeContent')
  var windows_folders = windows_tree.find('div.WindowsFolder')
  var active_folder = windows_tree.find('.FolderOpen')
  var active_folderindex = windows_folders.index(active_folder)
  var active_foldername = active_folder.html()
  windows_folders.eq(active_folderindex).after('<input onkeyup="restrictFolderName(this)" onblur="windowsRename2(this)" value="' + active_foldername + '" type="text" />').css('display', 'none')
  windows_tree.find('input').focus()
}

function restrictFolderName(target) {
  if ($(target).val().length > 40) {
    $(target).attr('value', $(target).val().substring(0, 40))
  }
  var unicode = window.event.keyCode
  //alert(unicode)
  if (unicode == 13) {
    windowsRename2(target)
  }
}

function windowsRename2(target) {
  var new_name = $(target).val()
  $(target).prev('div').html($(target).val()).css('display', 'block')
  var folder_id = $(target).prev('div').attr('class').substring(10, $(target).prev('div').attr('class').indexOf(' '))
  $(target).remove()
  var ajax_command;
  var visible_center = $('.PanelCenter:visible').attr('id')
  $('#ContentFolderName').html('in ' + $(target).val())
  var active_screen = $('.ManageScreenOpen').parent().parent().parent().attr('id')
  if (location.href.indexOf('Download.aspx') == -1) {
    if (active_screen == 'Panel2') {
      ajax_command = 'editAssetContentFolder'
    }
    else if (active_screen == 'Panel3') {
      ajax_command = 'editSlideFolder'
    }
    else {
    }
  }
  else {
    ajax_command = 'renamePC'
  }
  var ajaxstuff = ajax_command + ',,' + folder_id + ',,' + new_name
  //alert(ajaxstuff)
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    //alert(data)
  })
}

function windowsClose(clicked) {
  $('a.ManageScreenOpen').removeClass('.ManageScreenOpen')
  if (location.href.indexOf('Download.aspx') == -1) {
    reverseWindows()
  }
  else {
    dropDownAjax('DropDownPCs')
  }
}

function reverseWindows() {
  var property_title = $('#PropertiesTitle').html()
  var win_folders = $('#DivPopUpProperties:visible div.WindowsTreeContent div.WindowsFolder')
  var win_folder_length = win_folders.length;
  var folder_names = '';
  for (var x = 0; x < win_folder_length; x++) {
    folder_names += '<option value="' + win_folders.eq(x).attr('class').substring(10, win_folders.eq(x).attr('class').indexOf(' ')) + '">' + win_folders.eq(x).html() + '</option>'
  }
  if (property_title.indexOf('Content') > 0) {
    var win_drop = $('#DropDownFolders')
  }
  else {
    var win_drop = $('#ForJQSlideDropDown')
  }
  if (win_folder_length > 0) {
    win_drop.find('select').html(folder_names)
    //dropDownStreamPopulate(win_drop)
    win_drop.find('select').change(function() {
      dropDownStreamPopulate(this)
    });
  }
  else {
    if (win_drop.is('#DropDownFolders')) {
      createDropDownAjax(0)
    }
    else {
      createDropDownAjax(1)
    }
  }
}

function addWindowsFolder() {
  var content_folder = 1
  $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.WindowsFolder').each(function() {
    if ($(this).html() == 'New Folder ' + content_folder) {
      content_folder++
    }
  });
  $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent').append('<div class="WindowsFolder">New Folder ' + content_folder + '</div>')
  $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.WindowsFolder:last').bind('click', windowsFolder)
  $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.WindowsFolder:last').bind('dblclick', windowsRename)
  $('div.WindowsView:visible div.WindowsHolder:last').after('<div class="WindowsHolder"></div>')
  $('div.WindowsView:visible div.WindowsTree .WindowsTreeContent div.WindowsFolder:last')
		.bind("dropstart", function(event) {
		  $(this).addClass("FolderActive");
		})
		.bind("drop", function(event) {
		  var this_index = $(this).parent().find('.WindowsFolder').index(this)
		  var drop_here = $(this).closest('div.WindowsView').find('div.WindowsHolder:eq(' + this_index + ')')
		  drop_here.append(event.dragTarget);
		})
		.bind("dropend", function(event) {
		  $(this).removeClass("FolderActive");
		});

}


function cloneFields(field_id) {
  $('#' + field_id).clone().insertAfter($('#' + field_id))
}
function showFields(field_id) {
  $('.' + field_id + ' .RFormBox:hidden:first').slideDown(300)
  $('.' + field_id + ' .InBetweenRForms:hidden:first').slideDown(300)
}

function initDocumentView() {
  $('div.WindowsView div.WindowsFolder:eq(0)').addClass('FolderOpen')
  $('div.WindowsView div.WindowsHolder:eq(0)').css('display', 'block')
  $("div.WindowsView .WindowsFile").click(function() {
    if ($(this).is('.WindowsSelectedFile')) {
      $(this).removeClass('WindowsSelectedFile')
    }
    else {
      $(this).addClass('WindowsSelectedFile')
    }
    $('div.WindowsFileDepo h2 a').html('Select all')
  });
  //var temp_var = 0;
  $("div.WindowsView .WindowsFile")
		.bind("dragstart", function(event) {
		  var $drag = $(this), $proxy = $drag.clone(true, true);
		  $drag.css("opacity", "0.5").addClass('WindowsSelectedFile');
		  return $proxy.appendTo(document.body).addClass("ghost");
		})

		.bind("drag", function(event) {
		  $(event.dragProxy).css({
		    position: "absolute",
		    left: event.offsetX,
		    top: event.offsetY,
		    zIndex: '1000000',
		    opacity: "0.5"
		  });
		})
		.bind("dragend", function(event) {


		  $(event.dragProxy).fadeOut("normal", function() {
		    $(this).remove();
		  });
		  $(this).removeClass("outline");
		  $(this).css("opacity", "1");

		});
  $('div.WindowsView:visible .WindowsFolder')
		.bind("dropstart", function(event) {
		  $(this).addClass("FolderActive");
		})
		.bind("drop", function(event) {
		  var selected_files = $('div.WindowsView .WindowsSelectedFile:visible');
		  var content_id = $(this).attr('class').substring(9, $(this).attr('class').indexOf(' '));
		  var folder_to = $('.FolderActive').attr('class').substring(10, $(this).attr('class').indexOf(' '));
		  var folder_from = $('.FolderOpen').attr('class').substring(10, $(this).attr('class').indexOf(' '));
		  var stream_str = '';
		  var this_index = $(this).parent().find('.WindowsFolder').index(this)
		  var drop_here = $(this).closest('div.WindowsView').find('div.WindowsHolder:eq(' + this_index + ')')
		  var drop_here_natives = drop_here.find('.WindowsFile')
		  if (selected_files.length > 0) {
		    for (var p = 0; p < selected_files.length; p++) {
		      var duplicate_content = 0;

		      if (duplicate_content != 1) {
		        stream_str += selected_files.eq(p).attr('class').substring(9, selected_files.eq(p).attr('class').indexOf(' ')) + '||'
		      }
		    };
		    //alert(stream_str)
		    stream_str = stream_str.substring(0, (stream_str.length - 2))
		  }
		  var ajax_command;
		  if (location.href.indexOf('Download.aspx') != -1) {
		    ajax_command = 'movePCStreams'
		  }
		  else {
		    if ($('div.Panel a.ManageScreenOpen').closest('.Panel').is('#Panel3')) {
		      ajax_command = 'moveSlideContent'
		    }
		    else {
		      ajax_command = 'moveRawContent'
		    }
		  }


		  var ajaxstuff = ajax_command + ',,' + folder_from + ',,' + folder_to + ',,' + stream_str;
		  //alert(ajaxstuff)
		  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
		    //alert(data)
		  })

		  drop_here.append(selected_files);
		  $('.WindowsSelectedFile').removeClass('WindowsSelectedFile')
		  $('.NotDuplicate').removeClass('.NotDuplicate')
		  //drop_here.append(selected_files);
		})
		.bind("dropend", function(event) {
		  $(this).removeClass("FolderActive");
		});
}


function popScheduleInfo(clicked) {
  $('.Scheduling').removeClass('Scheduling')
  $(clicked).addClass('Scheduling')
  $('#DivPopUpTimings:first input').removeAttr('value')
  $('#DivPopUpTimings:first div.RepeatedField div.RFormBox').css('display', 'none')
  $('#DivPopUpTimings:first div.RepeatedField div.InBetweenRForms').css('display', 'none')
  $('#DivPopUpTimings:first div.RepeatedField div.RFormBox:first').css('display', 'block')
  $('#DivPopUpTimings:first div.RepeatedField div.InBetweenRForms:first').css('display', 'block')
  var current_id = $(clicked).parent().find('div.StreamMeta span.MetaKey').html()
  var timestamp = new Date().getTime()
  //alert('?command=' + command_type + '&' + id_type + '=' + content_id + '&' + timestamp)
  //alert('?command=getChannelSlideProperties&slideID=' + current_id + '&' + timestamp)
  $.get(ajax_path_get + '?command=getChannelSlideProperties&slideID=' + current_id + '&' + timestamp, function(data) {
    //alert(data)
    var data_array = eval("(" + data + ")");
    $('#TimingsURL').attr('value', unMicEncode(data_array[0][0]))
    $('#TimingsDur').attr('value', data_array[0][1])
    var weekdays = $('#DivPopUpTimings div.WeekDaysList input')
    for (var day_of_week = 0; day_of_week < data_array[1].length; day_of_week++) {
      if (data_array[1][day_of_week] == '1') {
        weekdays.eq(day_of_week).attr('checked', 'checked')
      }
    }
    for (var f = 2; f < (data_array.length); f++) {
      if (f != 2) {
        $('#DivPopUpTimings:first .RepeatedField .RFormBox:hidden:first').css('display', 'block')
        $('#DivPopUpTimings:first .RepeatedField .InBetweenRForms:hidden:first').css('display', 'block')
      }
      var start_date = data_array[f][0]
      var end_date = data_array[f][1]
      var start_time = data_array[f][2]
      var end_time = data_array[f][3]
      var property_inputs = $('#DivPopUpTimings:first div.RepeatedField div.RFormBox:visible:last .TextBox')
      property_inputs.eq(0).val(start_date)
      property_inputs.eq(1).val(end_date)
      property_inputs.eq(2).val(start_time)
      property_inputs.eq(3).val(end_time)
    }
  });
}

function sendScheduleInfo(clicked) {
  var current_id = $('div.Scheduling').parent().find('div.StreamMeta span.MetaKey').html()
  var url = ',,' + micEncode($('#TimingsURL').val())
  var duration = ',,' + $('#TimingsDur').val()
  var days_str = ''
  $('#DivPopUpTimings div.WeekDaysList:first input').each(function() {
    if ($(this).is(':checked')) {
      days_str += '1'
    }
    else {
      days_str += '0'
    }
  });
  var schedule_str = ''
  $('#DivPopUpTimings:first .RepeatedField .RFormBox:visible').each(function() {
    var schedule_check = ''
    for (var s = 0; s < 4; s++) {
      if ($(this).find('input.TextBox:eq(' + s + ')').val().length == 0) {
        schedule_check = schedule_check + '0'
      }
      else {
        schedule_check = schedule_check + '1'
      }
    }
    if (schedule_check.indexOf('1') != -1) {
      schedule_str += ',,' + $(this).find('input.TextBox:eq(0)').val() + '||' + $(this).find('input.TextBox:eq(1)').val() + '||' + $(this).find('input.TextBox:eq(2)').val() + '||' + $(this).find('input.TextBox:eq(3)').val()
    }
  });
  var ajaxstuff = 'editChannelSlideProperties,,' + current_id + url + duration + ',,' + days_str + schedule_str
  //alert(ajaxstuff)
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    if (data == -1) {
      $('#DivPopUpTimings:first .OxiValidation').html('Display duration is not valid.').slideDown(200)
    }
    else if (data == -2) {
      $('#DivPopUpTimings:first .OxiValidation').html('Invalid date/time.').slideDown(200)
    }
    else if (data == -3) {
      $('#DivPopUpTimings:first .OxiValidation').html('Display duration is out of bounds.').slideDown(200)
    }
    else {
      unBlackOut3($(clicked))
    }
  })
}

function multiSchedule() {

    var ajaxstuff = 'addChannelContents,,' + $('#ForJQStreamDropDown option:selected').val() + ',,' + micEncode($('#MultiSchedURL').val()) + ',,' + $('#MultiSchedDur').val() + ',,' + $('#MultiSchedSD2 input').val() + ',,' + $('#MultiSchedED2 input').val() + ',,' + $('#MultiSchedST').val() + ',,' + $('#MultiSchedET').val() + ',,'
  var ajax_days = ''
  $('#MultiSchedDays input').each(function() {
    if ($(this).is(':checked')) {
      ajax_days = ajax_days + '1'
    }
    else {
      ajax_days = ajax_days + '0'
    }
  });
  ajaxstuff = ajaxstuff + ajax_days + ',,'
  $('#Panel3 .SelectedStream').each(function() {
    ajaxstuff = ajaxstuff + $(this).find('.MetaKey').html() + '||'
  });
  //alert(ajaxstuff.length)
  ajaxstuff = ajaxstuff.substring(0, (ajaxstuff.length - 2))
  //alert(ajaxstuff)
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    //alert(data)
    //createDropDownAjax()
    //$('div.SelectedStream').removeClass('SelectedStream')
    checkCentreColumn2()

  })
}

function manageScreenTransfer(clicked) {
  $('.ManageScreenOpen').removeClass('ManageScreenOpen')
  $(clicked).addClass('ManageScreenOpen')

  var folder_names = $(clicked).parent().parent().find('.PanelDropDown option')
  var folder_length = $(clicked).parent().parent().find('.PanelDropDown option').length
  var windows_folders = new Array()
  var windows_id = new Array()
  var windows_tree = $('#DivPopUpProperties .WindowsTree .WindowsTreeContent')
  var windows_depo = $('#DivPopUpProperties .WindowsFileDepo')
  windows_tree.find('.WindowsFolder').remove()
  windows_depo.find('.WindowsHolder').remove()
  var check_panel = $(clicked).parent().parent().parent()
  if (check_panel.is('.Panel')) {
    if (check_panel.is('.Panel2')) {
      $('#PropertiesTitle').html('Manage your Content Folders')
      $('#PropertiesFolders').html('My Content Folders')
      $('#PropertiesContent').html('<a href="#" onclick="windowsSelectAll();return false;">Select all</a>My Content <span id="ContentFolderName">in ' + folder_names.eq(0).html() + '</span>')
      var command_type = 'rawContent'
      var command_type2 = 'rawContentAll'
    }
    else if (check_panel.is('.Panel3')) {
      $('#PropertiesTitle').html('Manage your Slide Folders')
      $('#PropertiesFolders').html('My Slide Folders')
      $('#PropertiesContent').html('<a href="#" onclick="windowsSelectAll();return false;">Select all</a>My Slides')
      var command_type = 'streamSlides'
      var command_type2 = 'slideContentAll'
    }
    var folder_type = 'folder'
  }
  else {
    var command_type = 'pcStreams'
    var folder_type = 'pc'
    var command_type2 = 'pcStreamsAll'
  }
  var pc_array = new Array();
  var window_folder_html = ''
  for (var f = 0; f < folder_length; f++) {
    windows_folders[f] = folder_names.eq(f).html()
    windows_id[f] = folder_names.eq(f).val()
    window_folder_html += '<div class="AjaxFolder' + windows_id[f] + ' WindowsFolder">' + windows_folders[f] + '</div>'
    var pc_id = folder_names.eq(f).val()
    pc_array[f] = folder_names.eq(f).val()
    if (folder_names.eq(f).data('phantom') == '1') {
      windows_tree.find(' div.WindowsFolder').addClass('PhantomPC')
    }
  }
  windows_tree.append(window_folder_html)
  var ajax_array = new Array();
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=' + command_type2 + '&' + timestamp, function(data) {
    var data_array = eval("(" + data + ")")
    var logout_success = 0;
    var folder_data = ''
    for (var q = 0; q < data_array.length; q++) {
      folder_data += '<div class="WindowsHolder">'
      for (var r = 0; r < data_array[q].length; r++) {
        var file_id = data_array[q][r][0]
        var file_name = data_array[q][r][1]
        var file_img = data_array[q][r][2]
        file_name = file_name.replace('||', '"')
        folder_data += '<div class="StreamKey' + file_id + ' WindowsFile"><img src="' + file_img + '" alt="' + file_name + '" title="' + file_name + '"/>' + file_name + '</div>'
      }
      folder_data += '</div>'
    }
    $('div.WindowsFileDepo').append(folder_data)
    initDocumentView()
    createWindows();
    $('.WindowsLoader').remove()
  })
}

function windowsSelectAll() {
  var visible_files = $('div.WindowsHolder:visible div.WindowsFile')
  var select_link = $('div.WindowsFileDepo h2 a')
  if (visible_files.length != $('div.WindowsHolder:visible div.WindowsSelectedFile').length) {
    visible_files.addClass('WindowsSelectedFile')
    select_link.html('Deselect all')
  }
  else {
    visible_files.removeClass('WindowsSelectedFile')
    select_link.html('Select all')
  }
}

function popStreamProperties() {
  var current_id = $('#ForJQStreamDropDown option:selected').attr('value')
  var timestamp = new Date().getTime()
  $.get(ajax_path_get + '?command=getStream&streamID=' + current_id + '&' + timestamp, function(data) {
    var ajax_array = new Array()
    ajax_array = data.split(',,')
    var cat_id = ajax_array[0]
    var cat_name = ajax_array[1]
    var stream_id = ajax_array[2]
    var stream_name = ajax_array[3]
    var stream_desc = ajax_array[4]
    var stream_desc2 = ajax_array[5]
    var stream_keywords = ajax_array[6].split('|')
    var keyword_length = stream_keywords.length
    var stream_locked = ajax_array[7]
    var stream_password = ajax_array[8]
    var stream_accept = ajax_array[9]
    var stream_private_before = ajax_array[10]
    $('#PrivateHiddenVar').html(stream_private_before)
    var property_input = $('#DivPopUpStreamProperties input')
    var property_text = $('#DivPopUpStreamProperties textarea')
    property_input.removeAttr('value')
    property_text.removeAttr('value').empty()
    property_input.eq(0).val(stream_name)
    property_text.eq(0).val(stream_desc)
    property_text.eq(1).val(stream_desc2)
    property_input.eq(1).val(stream_keywords[0])
    property_input.eq(2).val(stream_keywords[1])
    property_input.eq(3).val(stream_keywords[2])
    property_input.eq(4).val(stream_keywords[3])
    property_input.eq(5).val(stream_keywords[4])
    property_input.eq(6).val(stream_keywords[5])
    property_input.eq(9).val(stream_password)
    property_input.eq(10).val(stream_password)
    if (stream_locked == ',True' || stream_locked == 'True') {
      property_input.eq(8).attr('checked', 'checked')
      $('.PrivateHidden').css('display', 'block')
      $('.PrivateHiddenExtra').css('display', 'none')
    }
    else {
      property_input.eq(7).attr('checked', 'checked')
      $('.PrivateHidden').css('display', 'none')
      $('.PrivateHiddenExtra').css('display', 'none')
    }
    if (stream_accept == 'False') {
      property_input.eq(15).attr('checked', 'checked')
    }
    else {
      property_input.eq(14).attr('checked', 'checked')
    }
    $('#DivPopUpStreamProperties input:last').val(stream_id)
    if (cat_name.length < 1) {
      $('#BrowseTree').css('display', 'none')
      $('#BrowseNav').css('display', 'block')
    }
    else {
      $('#BrowseTree').css('display', 'block').html('<span id="' + cat_id + '">Your stream is currently in ' + cat_name + '. - <a href="#" onclick="changeCategory();return false;">Change</a></span>')
      $('#BrowseNav').css('display', 'none')
    }
  });
  $('#StreamPropertiesButton a:eq(0)').css('display', 'block')
  $('#StreamPropertiesButton a:eq(1)').css('display', 'none')
}

function changeCategory() {
  $('#BrowseTree').slideUp(300)
  $('#BrowseNav').slideDown(300)
  $('#BrowseNav .BrowseMain:eq(0)').click()
}

function popnewStreamProperties() {
  $('.PrivateHiddenExtra').css('display', 'none')
  $('#DivPopUpStreamProperties input').removeAttr('value')
  $('#DivPopUpStreamProperties textarea').removeAttr('value').empty()
  $('#StreamPropertiesButton a:eq(0)').css('display', 'none')
  $('#StreamPropertiesButton a:eq(1)').css('display', 'block')
  $('#DivPopUpStreamProperties #HiddenEnterString input').attr('value', 'newStreamProperties')
  $('#BrowseTree').css('display', 'none')
  $('#BrowseNav').css('display', 'block')
  $('#BrowseNav .BrowseMain:eq(0)').click()
}

function sendStreamProperties() {
  var cat_id = $('#BrowseNav a.BrowseMain:last').attr('id')
  var stream_id = $('#DivPopUpStreamProperties input:last').val()
  var property_input = $('#DivPopUpStreamProperties input')
  var property_text = $('#DivPopUpStreamProperties textarea')
  var stream_name = property_input.eq(0).val()
  var stream_desc = property_text.eq(0).val()
  var stream_desc2 = property_text.eq(1).val()
  var stream_keywords = property_input.eq(1).val() + '|' + property_input.eq(2).val() + '|' + property_input.eq(3).val() + '|' + property_input.eq(4).val() + '|' + property_input.eq(5).val() + '|' + property_input.eq(6).val()
  if (property_input.eq(8).attr('checked') == true) {
    var stream_locked = 'True'
  }
  else {
    var stream_locked = 'False'
  }
  var stream_password = property_input.eq(9).val()
  var stream_password2 = property_input.eq(10).val()
  if (property_input.eq(15).attr('checked') == true) {
    var stream_accept = 'False'
  }
  else {
    var stream_accept = 'True'
  }
  if (cat_id == null) {
    cat_id = -1
  }
  var locking_action = $('div.PrivateHidden div.PrivateHiddenExtra table.RbVerticalList input').index($('div.PrivateHidden div.PrivateHiddenExtra table.RbVerticalList input:checked'))
  var ajaxstuff = 'putStream,,' + cat_id + ',,' + stream_id + ',,' + stream_name + ',,' + stream_desc + ',,' + stream_desc2 + ',,' + stream_keywords + ',,' + stream_locked + ',,' + stream_password + ',,' + stream_accept + ',,' + locking_action
  if (stream_password == stream_password2) {
    if ($('.PrivateHiddenExtra').is(':hidden')) {
      var lock_action_var = 1;
    }
    else {
      if (locking_action == -1) {
        $('#DivPopUpStreamProperties .OxiValidation').html('Please select a locking action').slideDown(300)
        return false;
      }
    }
    $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
      if (data == -1) {
        $('#DivPopUpStreamProperties .OxiValidation').html('You must enter a stream name').slideDown(300)
      }
      else if (data == -2) {
        $('#DivPopUpStreamProperties .OxiValidation').html('You must enter a password').slideDown(300)
      }
      else {
        unBlackOut2v2()
        $('#ForJQStreamDropDown select option:selected').html(stream_name)
      }
    })
  }
  else {
    $('#DivPopUpStreamProperties .OxiValidation').html('Passwords do not match').slideDown(300)
  }
}

function checkPrivateHidden() {
  var locking_var = $('#PrivateHiddenVar').html()
  $('.PrivateHiddenExtra').css('display', 'block')
  if (locking_var == 'True') {
    $('div.PrivateHiddenExtra table tr:last').css('display', 'block')
  }
  else {
    $('div.PrivateHiddenExtra table tr:last').css('display', 'none')
  }
}

function checkPrivateHidden2() {
    $('div.PrivateHiddenExtra table tr:last').css('display', 'none')
}

function newStreamProperties() {
  var cat_id = $('#BrowseTree span:last').attr('id')
  var property_input = $('#DivPopUpStreamProperties input')
  var property_text = $('#DivPopUpStreamProperties textarea')
  var stream_name = property_input.eq(0).val()
  var stream_desc = property_text.eq(0).val()
  var stream_desc2 = property_text.eq(1).val()
  var stream_keywords = property_input.eq(1).val() + '|' + property_input.eq(2).val() + '|' + property_input.eq(3).val() + '|' + property_input.eq(4).val() + '|' + property_input.eq(5).val() + '|' + property_input.eq(6).val()
  if (property_input.eq(8).attr('checked') == true) {
    var stream_locked = 'True'
  }
  else {
    var stream_locked = 'False'
  }
  var stream_password = property_input.eq(9).val()
  var stream_password2 = property_input.eq(10).val()
  if (property_input.eq(15).attr('checked') == true) {
    var stream_accept = 'False'
  }
  else {
    var stream_accept = 'True'
  }
  if (cat_id == null) {
    cat_id = -1
  }
  var ajaxstuff = 'addStream,,' + cat_id + ',,' + stream_name + ',,' + stream_desc + ',,' + stream_desc2 + ',,' + stream_keywords + ',,' + stream_locked + ',,' + stream_password + ',,' + stream_accept
  if (stream_password == stream_password2) {
    $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
      unBlackOut2v2()
      createDropDownAjax(2)
      $('#DivPopUpStreamProperties #HiddenEnterString input').attr('value', 'sendStreamProperties')
    })
  }
  else {
    $('#DivPopUpStreamProperties .OxiValidation').html('Passwords do not match').slideDown(300)
  }
}

function popUpRemoveStream() {
  $('div.DivPopUpDeleteAlert').css({ display: 'block', zIndex: 99999 })
  $('#DivPopUpStreamProperties').css({ zIndex: 100 })
}
function unPopUpRemoveStream() {
  $('div.DivPopUpDeleteAlert').css({ display: 'none' })
  $('#DivPopUpStreamProperties').css({ zIndex: 99999 })
}

function removeStreamAjax() {
  var stream_id = $('#DivPopUpStreamProperties input:last').val()
  var ajaxstuff = 'removeStream,,' + stream_id
  $.post(ajax_path_put, { command: ajaxstuff }, function(data) {
    $('div.DivPopUpDeleteAlert').css({ display: 'none' })
    globalClosePopUp();
    createDropDownAjax(2);
  })
}

$(document).ready(function() {

  $('.OxiValidation').each(function() {
    if ($(this).text().length > 0) {
      $(this).css('display', 'block')
    }
  });

  //Highlight on focus - selects, textareas, textboxes
  $("select.WithFocusHighlight, textarea.WithFocusHighlight, input.WithFocusHighlight").focus(function() {
    $(this).closest(".FormBox, .FormBoxFullWidth").addClass("FormBoxHighlighted");
  });
  $("select.WithFocusHighlight, textarea.WithFocusHighlight, input.WithFocusHighlight").blur(function() {
    $(this).closest(".FormBox, .FormBoxFullWidth").removeClass("FormBoxHighlighted");
  });

  //Highlight on focus - single checkbox/radiobutton
  $("span.WithFocusHighlight > input").focus(function() {
    $(this).closest(".FormBox, .FormBoxFullWidth").addClass("FormBoxHighlighted");
  });
  $("span.WithFocusHighlight > input").click(function() { //added onclick because of safari
    $(this).focus();
  });
  $("span.WithFocusHighlight > input").blur(function() {
    $(this).closest(".FormBox, .FormBoxFullWidth").removeClass("FormBoxHighlighted");
  });

  //Highlight on focus - radiobutton/checkbox lists
  $("table.WithFocusHighlight").find("input").focus(function() {
    $(this).closest(".FormBox, .FormBoxFullWidth").addClass("FormBoxHighlighted");
  });
  $("table.WithFocusHighlight").find("input").click(function() { //added onclick because of safari
    $(this).focus();
  });
  $("table.WithFocusHighlight").find("input").blur(function() {
    $(this).closest(".FormBox, .FormBoxFullWidth").removeClass("FormBoxHighlighted");
  });

  //validation highlight
  $(".WithFocusHighlight").closest(".FormBox").find(".ValidationInfo").each(function() {
    if ($(this).closest(".InnerFormBox").length == 1) {
      $(this).closest(".InnerFormBox").find("input").addClass("InputFieldHighlighted");
      $(this).closest(".InnerFormBox").find("select").addClass("InputFieldHighlighted");
      $(this).closest(".InnerFormBox").find("textarea").addClass("InputFieldHighlighted");
    } else {
      $(this).closest(".FormBox").find("input").addClass("InputFieldHighlighted");
      $(this).closest(".FormBox").find("select").addClass("InputFieldHighlighted");
      $(this).closest(".FormBox").find("textarea").addClass("InputFieldHighlighted");
    }
  });




  $('#divStreamPreview').hover(function() {
    $('.StreamPreviewClose').stop(true, true).slideDown(300)
  }, function() {
    $('.StreamPreviewClose').slideUp(300)
  });

  $('select.DropDownYear').each(function() {
    var this_year = new Date().getFullYear()
    for (var d = 0; d < 150; d++) {
      curr_year = this_year - d
      $(this).append('<option>' + curr_year + '</option>')
    }
  });

  $('.WindowsFileDepo').disableSelection()

  //    $(document).bind('mousemove', function(e) {
  //      $("#OTHERTEST").text("Mouse X: " + e.pageX + ", Mouse Y: " + e.pageY);
  //    });

  $('.PublicPrivateShow input').click(function() {
    var locking_var = $('#PrivateHiddenVar').html()
    var this_index = $('.PublicPrivateShow input').index(this)
    var hidden_div = $(this).parent().parent().parent().parent().parent().parent().find('div.PrivateHidden')
    if (this_index == 1) {
      if (locking_var == 'True') {
        $('.PrivateHiddenExtra').css('display', 'block').find('table.RbVerticalList tr:last').css('display', 'block')
      }
      else {
        $('.PrivateHiddenExtra').css('display', 'block').find('table.RbVerticalList tr:last').css('display', 'none')
      }
      hidden_div.slideDown(400)
    }
    else {
      hidden_div.slideUp(400)
    }
  });

  $('div.PanelCenterContent').css('opacity', '0.2')


  $('.RFormBox:not(.NoFocusEffect) *').focus(function() {
    $('.RSelectedFormBox').removeClass('RSelectedFormBox')
    $(this).closest('.RFormBox').addClass('RSelectedFormBox')
    $(this).addClass('RSelectedInput')
  }).blur(function() {
    $(this).closest('.RFormBox').removeClass('RSelectedFormBox')
    $(this).removeClass('RSelectedInput')
  }).click(function() {
    $('.RSelectedFormBox').removeClass('RSelectedFormBox')
    $(this).closest('.RFormBox').addClass('RSelectedFormBox')
    $(this).addClass('RSelectedInput')
  }); ;

  $('.RFormBox *').bind('keypress', setAlert)


  $('.BrowseNav').mouseleave(function() {
    initBrowseNav()
  });
  //$('.NavHolder').bind('click', killNav)
  initBrowseHoverEffects()
  $('.TextBoxSpecial').bind('focus', textBoxSpecial)

  $('#CreateNextButton').click(function() {
    var inner_pos = $('.SlidingPanelsInner').css('left')
    if (inner_pos == '0px') {
      updateArrowNav(1)
    }
    if (inner_pos == '-515px') {
      updateArrowNav(2)
    }
    if ($('#CreateNextButton').html().indexOf('Finished') > -1) {
      window.location.href = '../CreateSuccess.aspx'
    }
  });

  $('.FooterSlideInner img').hover(function() {
    $(this).css('z-index', '1000')
    $(this).parent().css('z-index', '1000')
    $(this).stop().animate({ top: '-15px', left: '-15px', height: '80px', width: '80px' }, 200)
  }, function() {
    $(this).css('z-index', '10')
    $(this).parent().css('z-index', '0')
    $(this).stop().animate({ top: '0px', left: '0px', height: '50px', width: '50px' }, 100)
  });

  $('.DivPopUp:not(.DivPopUpLarge,.DivPopUpNoDrag)')
  .bind('drag', function(event) {
    $(this).css({
      left: event.offsetX,
      top: event.offsetY
    })
  });
  $('.DivPopUpAlert')
  .bind('drag', function(event) {
    $(this).css({
      left: event.offsetX,
      top: event.offsetY
    })
  });

  $('div.DivPopUpClose:not(div.DivPopUpLarge .DivPopUpClose,div.DivPopUp .DivPopUpClose2)').click(function() {
    if ($(this).closest('div.DivPopUp').is('#UploadPopUp')) {
      createDropDownAjax();
    }
    if ($(this).closest('div.DivPopUp').find('span.IsChanged').html() == '1') {
      divAlertPopUp()
    }
    else {
      unBlackOut2();
    }
  });
  $('div.DivPopUpLarge .DivPopUpClose').click(function() {
    unUploaderBlackOut(this);
  });
  $('div.DivPopUp .DivPopUpClose2').click(function() {
    if ($(this).closest('div.DivPopUp').find('span.IsChanged').html() == '1') {
      divAlertPopUp()
    }
    else {
      unBlackOut3(this);
    }
  });
  var initial_position;
  $('#ForJSSlideInner a').mousedown(function() {
    initial_position = $('#ForJSSlideInner').css('left');
  }).click(function() {
    var new_position = $('#ForJSSlideInner').css('left');
    if (new_position == initial_position) {
    }
    else {
      return false;
    }

  });
  var initial_width = $('#ForJSSlideInner div').eq(0).width();
  var max_scroll = $('#ForJSSlideInner div').width()
  var excess_scroll = 950 - parseInt(initial_width);

  $('#ForJSSlideInner')
  .bind('drag', function(event) {
    var measure_length = $("#ScrollMeasure").css('width')
    if (parseInt(measure_length) <= 930) {
      measure_length = '930px'
    }
    var measure_new = (parseInt(measure_length) - 860) / 2;
    if (parseInt($(this).css('left')) <= -(max_scroll + max_scroll)) {
    }
    else if (parseInt($(this).css('left')) >= 0) {
    }
    else {
      $(this).css({
        left: event.offsetX - measure_new
      });
    }
  });

  /**** DRAG AND DROP FUNCTION ****/

  $(function() {
    $(".drag")
		.bind("dragstart", function(event) {

		  var $drag = $(this), $proxy = $drag.clone();
		  $drag.addClass("outline");
		  $drag.css("opacity", "0.5");
		  return $proxy.appendTo(document.body).addClass("ghost");
		})

		.bind("drag", function(event) {
		  $(event.dragProxy).css({
		    position: "absolute",
		    zIndex: "1000",
		    left: event.offsetX,
		    top: event.offsetY,
		    opacity: "0.5"
		  });
		})
		.bind("dragend", function(event) {
		  $(event.dragProxy).fadeOut("normal", function() {
		    $(this).remove();
		  });
		  $(this).removeClass("outline");
		  $(this).css("opacity", "1");

		});
    $('.drop')
		.bind("dropstart", function(event) {
		  $(this).addClass("active");
		})
		.bind("drop", function(event) {
		  $(this).append(event.dragTarget);
		})
		.bind("dropend", function(event) {
		  $(this).removeClass("active");
		});
    $('.drop2')
		.bind("dropstart", function(event) {
		  $(this).addClass("active");
		})
		.bind("drop", function(event) {
		  $(this).append(event.dragTarget);
		})
		.bind("dropend", function(event) {
		  $(this).removeClass("active");
		});
  });
});



function launchLogin() {
  $('document').ready(function() {
    //      if (window.location.href.indexOf('CreateWizard.aspx') != -1) {
    //        //alert($('#DivLoginBox').length)
    //        //BlackOut2('DivLoginBox');
    //      }
  });
}


function createRedirect() {
  //alert($('#HeaderLoggedIn').is(':hidden'))
  if ($('#HeaderLoggedIn').is(':hidden')) {
    BlackOut2('DivLoginBox');
    $('#ProgressLogin').addClass('SendMeToCreate')
  }
  else {
    window.location.href = '/CreateWizard.aspx'
  }
}

function unCreateRedirect() {
  $('.SendMeToCreate').removeClass('SendMeToCreate')
}

var channelslide_timer;
var slide_curr = 0;
function initchannelSlideShow() {
  $('div.ChannelSlideShow div.ChannelSlide:eq(0)').css('display', 'block')
  if ($('div.ChannelSlideShow div.ChannelSlide').length > 1) {
    channelslide_timer = setTimeout('channelSlideShow()', 5000)
    $('.DetailControls').css('display', 'block')
  }
}
function pauseChannelSlideShow() {
  $('.DetailPause').css('display', 'none')
  $('.DetailPlay').css('display', 'block')
  clearTimeout(channelslide_timer)
}
function playChannelSlideShow() {
  $('.DetailPause').css('display', 'block')
  $('.DetailPlay').css('display', 'none')
  channelslide_timer = setTimeout('channelSlideShow()', 5000)
}
function nextChannelSlideShow() {
  clearTimeout(channelslide_timer)
  $('.DetailPrev,.DetailNext').css('display', 'none')
  $('.DetailPrev2,.DetailNext2').css('display', 'block')
  var slide_length = $('div.ChannelSlideShow div.ChannelSlide').length
  if (slide_curr == slide_length - 1) {
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + slide_curr + ')').animate({ left: '-861px' }, 500, function() {
      $(this).css('display', 'none')
      $('.DetailPrev,.DetailNext').css('display', 'block')
      $('.DetailPrev2,.DetailNext2').css('display', 'none')

    })
    $('div.ChannelSlideShow div.ChannelSlide:eq(0)').css({ display: 'block', left: '861px' }).animate({ left: '0px' }, 500)
    slide_curr = 0
  }
  else {
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + slide_curr + ')').animate({ left: '-861px' }, 500, function() {
      $(this).css('display', 'none')
      $('.DetailPrev,.DetailNext').css('display', 'block')
      $('.DetailPrev2,.DetailNext2').css('display', 'none')

    })
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + (slide_curr + 1) + ')').css({ display: 'block', left: '861px' }).animate({ left: '0px' }, 500)
    slide_curr++
  }
  if ($('.DetailPlay').is(':hidden')) {
    channelslide_timer = setTimeout('channelSlideShow()', 5000)
  }
}
function prevChannelSlideShow() {
  clearTimeout(channelslide_timer)
  $('.DetailPrev,.DetailNext').css('display', 'none')
  $('.DetailPrev2,.DetailNext2').css('display', 'block')
  var slide_length = $('div.ChannelSlideShow div.ChannelSlide').length
  if (slide_curr == 0) {
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + slide_curr + ')').animate({ left: '861px' }, 500, function() {
      $(this).css('display', 'none')
      $('.DetailPrev,.DetailNext').css('display', 'block')
      $('.DetailPrev2,.DetailNext2').css('display', 'none')

    })
    $('div.ChannelSlideShow div.ChannelSlide:last').css({ display: 'block', left: '-861px' }).animate({ left: '0px' }, 500)
    slide_curr = slide_length - 1
  }
  else {
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + slide_curr + ')').animate({ left: '861px' }, 500, function() {
      $(this).css('display', 'none')
      $('.DetailPrev,.DetailNext').css('display', 'block')
      $('.DetailPrev2,.DetailNext2').css('display', 'none')
    })
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + (slide_curr - 1) + ')').css({ display: 'block', left: '-861px' }).animate({ left: '0px' }, 500)
    slide_curr--
  }
  if ($('.DetailPlay').is(':hidden')) {
    channelslide_timer = setTimeout('channelSlideShow()', 5000)
  }
}
function channelSlideShow() {
  var slide_length = $('div.ChannelSlideShow div.ChannelSlide').length
  if (slide_curr == slide_length - 1) {
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + slide_curr + ')').animate({ left: '-861px' }, 500, function() {
      $(this).css('display', 'none')
    })
    $('div.ChannelSlideShow div.ChannelSlide:eq(0)').css({ display: 'block', left: '861px' }).animate({ left: '0px' }, 500)
    slide_curr = 0
  }
  else {
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + slide_curr + ')').animate({ left: '-861px' }, 500, function() {
      $(this).css('display', 'none')
    })
    $('div.ChannelSlideShow div.ChannelSlide:eq(' + (slide_curr + 1) + ')').css({ display: 'block', left: '861px' }).animate({ left: '0px' }, 500)
    slide_curr++
  }
  channelslide_timer = setTimeout('channelSlideShow()', 5000)
}


function checkDuration(target) {
  if (isNaN($(target).val()) == true) {
    $(target).attr('value', $(target).val().substring(0, ($(target).val().length - 1)))
    checkDuration(target)
  }
}




//initial example value in a form field; disappears on focus, appears back on blur if is empty
function clearField(field_to_clear, initial_value) {
  if (initial_value == field_to_clear.value) field_to_clear.value = "";
}
function fillField(field_to_clear, initial_value) {
  if (field_to_clear.value == "") field_to_clear.value = initial_value;
}

/********* SELECT WITH FILTER functions *********/
//function invoked onload at body tag; or just before closiong body tag
function initArrays() {
  select_objects = document.getElementsByTagName('select'); //must be global
  all_selects = new Array();  //must be global, 3 dimensional array [ids of all selects][counter of each option in given select]['value' or 'text']= value or text of given option in given select
  options_selected = new Array(); //must be global, 2 dimensional array [ids of selects on which onchange or onkeydown function was invoked][values of selected options for associative addressing]=true ,but this boolean value is not used, existence of array entry is tested.

  for (var i = 0; i < select_objects.length; i++) {
    var index = select_objects[i].id;  // id of select tag
    all_selects[index] = new Array();
    options_selected[index] = new Array();
    for (var j = 0; j < select_objects[i].length; j++) {  //number of options elements in given select tag
      all_selects[index][j] = new Array();
      all_selects[index][j]['value'] = select_objects[i].options[j].value;
      all_selects[index][j]['text'] = select_objects[i].options[j].text;
    } //for j
  } //for i
}

//function invoked onkeyup of input tag containing search phrase
function filterFunction(container_id, filter) {
  //alert('222')
  //container_id - id of a div containing related ddl
  //filter - reference to input tag containing filter phrase or "" for bringing back all options into select
  //sel - reference to related dropdownlist (select)
  var container_ref = document.getElementById(container_id);
  var sel = container_ref.getElementsByTagName("select")[0];
  if (sel) {
    var select_id = sel.id;
  } else {
    return; //if sendForm gives unpredictable id of other listboxes that don't use filter
  }

  if (options_selected[select_id].length == 0)
    selectFunction(document.getElementById(select_id)); //remember already selected options, not only these selected manualy
  sel.options.length = 0;
  var k = 0;
  for (var i = 0; i < all_selects[select_id].length; i++) {
    var t = all_selects[select_id][i]['text'];
    var t_for_search = t.toLowerCase();
    var v = all_selects[select_id][i]['value'];
    var filter_text = (typeof filter == "object") ? filter.value.toLowerCase() : '';
    if (t_for_search.indexOf(filter_text) >= 0)  // == to search from the begining of the listbox item
      sel.options[k++] = new Option(t, v, false, (typeof options_selected[select_id][v] != "undefined"));
  }
}

//function invoked onchange at select tag, to remember the selected items
function selectFunction(this_select) {
  var number_of_options = this_select.options.length;  //in given select
  for (var i = 0; i < number_of_options; i++) {
    if (this_select.options[i].selected == true) {
      options_selected[this_select.id][this_select.options[i].value] = true;
    } else {
      if (typeof options_selected[this_select.id][this_select.options[i].value] != "undefined")
        delete options_selected[this_select.id][this_select.options[i].value];
    }
  }
}

/******* end SELECT WITH FILTER functions ********/

function adminAddStreams(from, to) {
  $('#' + from).find('select option:selected').each(function() {
    $('#' + to).find('select').append('<option value="' + $(this).val() + '">' + $(this).html() + '</option>')
  });
}
function adminRemoveStreams(from) {
  $('#' + from).find('select option:selected').each(function() {
    $(this).remove()
  });
}

function adminPopStreamHidden(field, list) {
  var hidden_value = ''
  $('#' + list + ' select option').each(function() {
    hidden_value = hidden_value + $(this).val() + ',,'
  });
  var fixed_value = hidden_value.substr(0, (hidden_value.length - 2))
  $('#' + field).val(fixed_value)
}

function initAdminSchedule() {
  var left_col = $('#ForJSSchedStarts')
  var right_col = $('#ForJSSchedEnds')
  var start_fields = left_col.find('.AdminLeft')
  var end_fields = right_col.find('.AdminRight')
  start_fields.eq(0).css({ display: 'block', border: 'none' })
  end_fields.eq(0).css({ display: 'block', border: 'none' })
  start_fields.find('.EditBoxW1').each(function() {
    if ($(this).val().length > 0 && $(this).closest('.AdminLeft').length > 0) {
      var this_index = start_fields.index($(this).closest('.AdminLeft'))
      $(this).closest('.AdminLeft').css('display', 'block').prevAll('.AdminLeft').css('display', 'block')
      end_fields.eq(this_index).css('display', 'block').prevAll('.AdminRight').css('display', 'block')
    }
  });
  end_fields.find('.EditBoxW1').each(function() {
    if ($(this).val().length > 0 && $(this).closest('.AdminRight').length > 0) {
      var this_index = end_fields.index($(this).closest('.AdminRight'))
      $(this).closest('.AdminRight').css('display', 'block').prevAll('.AdminRight').css('display', 'block')
      start_fields.eq(this_index).css('display', 'block').prevAll('.AdminLeft').css('display', 'block')
    }
  });
}

function adminAddSchedule() {
  var left_col = $('#ForJSSchedStarts')
  var right_col = $('#ForJSSchedEnds')
  var start_fields = left_col.find('.AdminLeft:hidden')
  var end_fields = right_col.find('.AdminRight:hidden')
  start_fields.eq(0).slideDown(200)
  end_fields.eq(0).slideDown(200)
}

function adminCheckTemplate() {
  if ($('#ForJSDropDownCheck select').find('option').index($('#ForJSDropDownCheck').find('option:selected')) != 1) {
    $('#ForJSDropDownCheck2 select').attr('disabled', 'disabled')
  }
  else {
    $('#ForJSDropDownCheck2 select').removeAttr('disabled')
  }
}

function adminCheckPasswordRequest() {
  if ($('#ForJSRequest input:eq(0)').is(':checked')) {
    $('#ForJSRequest1 input').attr('disabled', 'disabled')
    $('#ForJSRequest2 input').attr('disabled', 'disabled')
  }
  else {
    $('#ForJSRequest1 input').removeAttr('disabled')
    $('#ForJSRequest2 input').removeAttr('disabled')
  }
}

//TEMP FOR ADMIN UNTIL BACK-END IS THERE
function addToLocalNav(tag, link) {
  $('.LocalNav').append('<div class="Inactive"><a href="' + link + '.aspx">' + tag + '</a></div>')
}

function removeFromLocalNav(str) {
  var str_array = str.split(',')
  var local_nav_links = $('.LocalNav div')
  for (var x = 0; x < str_array.length; x++) {
    local_nav_links.eq(str_array[x]).remove()
  }
}

function check(e) {

}

function findGoButton(function_name, function_var, e) {
  var code;
  if (!e) var e = window.event;
  if (e.keyCode) code = e.keyCode;
  else if (e.which) code = e.which;
  if (code == 13) {
    eval(function_name + '(' + function_var + ')')
  }
}

function findGoButton2(button_id) {
  var unicode = window.event.keyCode
  //alert(unicode)
  if (unicode == 13) {
    $('#' + button_id).click()
  }
}
function multiFindGo() {
  var unicode = window.event.keyCode
  //alert(unicode)
  if (unicode == 13) {
    var function_name = $('#HiddenEnterString input').val()
    eval(function_name + '()')
  }
}

function checkDownloadAuto() {
  var hidden_field = $('#ForJSDownloadHidden input').val()
  if (hidden_field.length > 0) {
    miniNavShow(1)
    $('.MiniNavDivs:eq(1) input').attr('value', hidden_field)
    searchAjax($('.MiniNavDivs:eq(1) a'))
  }
}

function globalClosePopUp() {
  if ($('.DivPopUpAlert,.DivPopUpAlert2,.DivPopUpDeleteAlert').is(':visible')) {
    //Do Nothing
  }
  else {
    $('#DivLoginTerms').css('display','none')
    $('.DivPopUp:visible .DivPopUpClose').click()
    $('#divStreamPreview .StreamPreviewClose a').click()
  }
}

function expandDefaultSettings() {
  var target = $('#DefaultSettingsInner')
  if (target.is(':visible')) {
    target.slideUp(200)
    $('#DefaultSettingsExpandLink a').addClass('Dropped')
  }
  else {
    target.slideDown(200)
    $('#DefaultSettingsExpandLink a').removeClass('Dropped')
  }
}


$(document).ready(function() {
  //$.timeEntry.setDefaults({ show24Hours: true, spinnerImage: '/images/default/spinnerDefault.png' });
});

function createArrowLeft() {
  if (whatpanel > 0) {
    updateArrowNav(whatpanel - 1)
  }
}

function createArrowRight() {
  if (whatpanel < 2) {
    updateArrowNav(whatpanel + 1)
  }
}

function schedAdvOptions() {
  var sched_opt = $('#SchedAdvOptions')
  if (sched_opt.is(':hidden')) {
    sched_opt.stop(true, true).slideDown(200, function() {
      $('a.SchedAdvOptionLink').addClass('SchedAdvOptionLinkOpen')
    })
  }
  else {
    sched_opt.slideUp(200)
    $('a.SchedAdvOptionLink').removeClass('SchedAdvOptionLinkOpen')
  }
}

function initFAQs() {
  $('.OxigenFAQ h2 a').click(function() {
    var this_content = $(this).parent().next('div')
    var other_content = $('.OxigenFAQ div:not(.OxigenFAQ div div)')
    if (this_content.is(':hidden')) {
      other_content.slideUp(300)
      $('.FAQOpen').removeClass('FAQOpen')
      this_content.slideDown(300)
      $(this).addClass('FAQOpen')
    }
    else {
      this_content.slideUp(300)
      $(this).removeClass('FAQOpen')
    }
    return false;
  });
}


function showTerms() {
  $('#DivLoginBox').css({ zIndex: '9' })
  $('#DivLoginTerms').css({ display: 'block', zIndex: '99999' })
}

function closeTerms() {
  $('#DivLoginBox').css({ zIndex: '99999' })
  $('#DivLoginTerms').css({ display: 'none'})
}

function micEncode(str) {
  var fixed_str = str;
  if (str.indexOf(',,') > -1) {
    fixed_str = str.replace(",,","{a001}")
  }
  return fixed_str;
}

function unMicEncode(str) {
  var fixed_str = str;
  if (str.indexOf('{a001}') > -1) {
    fixed_str = str.replace("{a001}", ",,")
  }
  return fixed_str;
}
