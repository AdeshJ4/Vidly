const express = require("express");
const app = express();
const dotenv = require("dotenv").config();

require("./startup/dbConnection")();
require("./startup/routes")(app, express);

app.get("/", (req, res) => {
  return res.status(200).send("Welcome to Vidly admin panel! (Automated)");
});

const port = process.env.PORT || 5001;

app.listen(port, () => {
  console.log(`server is listening on port ${port}`);
});
