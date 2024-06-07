const mongoose = require("mongoose");
const dotenv = require("dotenv").config();

const connectDB = async () => {
  try {
    const connect = await mongoose.connect(process.env.CONNECTION_STRING);
    console.log(`Database is connected`);
    console.log(`Host: ${connect.connection.host}`);
    console.log(`DB Name: ${connect.connection.name}`);
  } catch (err) {
    console.log(`Database is not Connected`);
    console.log(`Error: ${err.message}`);
    process.exit(1);
  }
};
module.exports = connectDB;





/**

In Node.js, process.exit(1) & process.exit(0) both used to exit the current process but difference is how (successfully or unsuccessfully)

1. process.exit(0): 

Process completed successfully and we exit.
Indicates a successful termination of the process.
Conventionally used to signal that the process completed without any errors.


2. process.exit(1):

Process completed unsuccessfully and we exit.
Indicates an unsuccessful termination of the process.
Conventionally used to signal that the process encountered an error or failed in some way.

example: 
changed database string from env file, instead of 127 make it 128 and run command using "node server.js";
*/