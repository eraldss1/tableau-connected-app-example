const { loadRoute } = require("./src/route");

require("dotenv").config();
const express = require("express");
const cors = require("cors");

const app = express();

app.use(cors());
loadRoute(app);

PORT = process.env.PORT || 3000;

app.listen(PORT, () => {
  console.log(`API is running on port ${PORT}`);
});
