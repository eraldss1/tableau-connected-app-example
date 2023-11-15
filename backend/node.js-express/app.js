const { loadRoute } = require("./src/route");

require('dotenv').config()
const express = require("express");
const cors = require('cors')

const app = express();
const port = 3000;

app.use(cors())
loadRoute(app);

app.listen(port, () => {
  console.log(`API is running on port ${port}`);
});
