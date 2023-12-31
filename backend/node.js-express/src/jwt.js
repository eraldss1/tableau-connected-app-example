// import { v4 as uuidv4 } from "uuid";

const { v4: uuidv4 } = require("uuid");

var jwt = require("jsonwebtoken");

module.exports = {
  getToken(req, res) {
    const username = req.query.username;
    const uuid = uuidv4();
    const timenow = new Date().getTime();
    const expiry = new Date().getTime() + 5 * 60 * 1000;
    const scope = ["tableau:views:embed", "tableau:metrics:embed"];

    var token = jwt.sign(
      {
        iss: process.env.CLIENT_ID,
        sub: username,
        aud: "tableau",
        exp: expiry / 1000,
        iat: timenow / 1000,
        jti: uuid,
        scp: scope,
      },
      process.env.SECRET_VALUE,
      {
        algorithm: "HS256",
        header: {
          kid: process.env.SECRET_ID,
          iss: process.env.CLIENT_ID,
        },
      }
    );
    res.send({ token: token });
  },
};
