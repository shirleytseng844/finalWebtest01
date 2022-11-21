$(document).ready(function(){
    $("#register1").click(function(){
      $("#login1").css("background-color", "#ecf0f1");
      $("#login1 > span").css("color", "#333");
      $("#register1").css("background-color", "#f26835");
      $("#register1 > span").css("color", "white");
      $("#l-f").toggle(500);
      $("#r-f").toggle(1000);
    })
    $("#login1").click(function(){
      $("#register1").css("background-color", "#ecf0f1");
      $("#register1 > span").css("color", "#333");
      $("#login1").css("background-color", "#f26835");
      $("#login1 > span").css("color", "white");
      $("#l-f").toggle(1000);
      $("#r-f").toggle(500);
    })
  });