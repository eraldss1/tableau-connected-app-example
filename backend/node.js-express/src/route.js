const { getGreeting } = require("./greet");
const { getToken } = require("./jwt");

module.exports = {
  loadRoute(app) {
    app.get("/greet", getGreeting);
    app.get("/token", getToken);
  },
};
