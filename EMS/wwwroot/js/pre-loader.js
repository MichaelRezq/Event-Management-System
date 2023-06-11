(function ($) {
  "use strict";

  /*============= preloader js css =============*/
  var cites = [];
  cites[0] =
    "Manage All Your Meatings Here.";
  cites[1] = "Manage All Your Meatings Here.";
  cites[2] = "Meet Investors For Your Company Here";
  cites[3] = "Meet Company Presenters and Invest your Money";
  var cite = cites[Math.floor(Math.random() * cites.length)];
  $("#preloader p").text(cite);
  $("#preloader").addClass("loading");

  $(window).on("load", function () {
    setTimeout(function () {
      $("#preloader").fadeOut(500, function () {
        $("#preloader").removeClass("loading");
      });
    }, 500);
  });
})(jQuery);
