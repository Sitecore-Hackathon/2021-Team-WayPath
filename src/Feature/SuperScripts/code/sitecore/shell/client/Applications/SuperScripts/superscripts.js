define(["sitecore"], function (Sitecore) {
  var superscripts = Sitecore.Definitions.App.extend({
      initialized: function () {
          var app = this;
          $(".scriptlink").click(function(e) {
              var id = Sitecore.Helpers.url.getQueryParameters(window.location.href)['scriptID'];
              e.preventDefault();
              var url = $(this).attr('href');

              app.Frame1.set("sourceUrl", url);
              app.DialogWindow1.show();
              
          });
      }
  });

    return superscripts;
});