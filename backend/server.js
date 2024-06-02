const express = require("express");
const app = express();
const dotenv = require("dotenv").config();

require('./startup/dbConnection')();


app.get("/", (req, res) => {
  return res.status(200).send("Home Page of Vidly Movie Application");
});

const port = process.env.PORT || 5001;
app.listen(port, () => {
  console.log(`server is listening on port ${port}`);
});
