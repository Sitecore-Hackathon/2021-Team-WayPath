define(["sitecore"], function (Sitecore) {
  var superscripts = Sitecore.Definitions.App.extend({
      initialized: function () {
          var app = this;
          $(".scriptlink").click(function (e) {
              var id = Sitecore.Helpers.url.getQueryParameters(window.location.href)['scriptID'];
              e.preventDefault();
              var url = $(this).attr('href');
            
              app.Frame1.set("sourceUrl", url);
              app.DialogWindow1.show();
          })
      },
      showScriptDialog: function (url) {
          try {
              //adding couple of querystrings as the dialog shows the content based on these parameters (passed from first page)
            
              var app = this;              
              var url = url;
            
              app.Frame1.set("sourceUrl", url);
              app.DialogWindow1.show();
          }
          catch (exception) {
              console.log("Error occured while loading the dialog: " + exception.name + ", " + exception.message + ", " + exception.stack)
          }
      }
  });

    return superscripts;
});