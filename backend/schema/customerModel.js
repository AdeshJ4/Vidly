const { default: mongoose } = require("mongoose");

const customerSchema = new mongoose.Schema({
  name: {
    type: String,
    required: true,
    trim: true,
  },
  email: {
    type: String,
    required: true,
    unique: true,
    trim: true,
  },
  phone: {
    type: String,
    required: true,
    trim: true,
  },
  dateOfBirth: {
    type: Date,
    required: true,
  },
  status: {
    type: String,
    enum: ["active", "inactive"],
    default: "active",
  },
  address: {
    street: { type: String, required: true },
    city: { type: String, required: true },
    state: { type: String, required: true },
    postalCode: { type: String, required: true },
    country: { type: String, required: true },
  },
  membership: {
    level: {
      type: String,
      enum: ["Bronze", "Silver", "Gold", "Platinum"],
      default: "Bronze",
    },
    startDate: {
      type: Date,
      default: Date.now,
    },
    endDate: {
      type: Date,
    },
    status: {
      type: String,
      enum: ["active", "expired"],
      default: "active",
    },
  },
});

const Customer = mongoose.model("Customer", customerSchema);

export default Customer;

/*

const newCustomer = new Customer({
  name: "John Doe",
  email: "john.doe@example.com",
  phone: "+1 (123) 456-7890",
  address: {
    street: "123 Main St",
    city: "Pune",
    state: "CA",
    postalCode: "12345",
    country: "USA",
  },
  dateOfBirth: "1980-01-01",
  profilePic: "https://example.com/images/john_doe.jpg",
  membership: {
    level: "Gold",
    startDate: new Date("2023-01-01"),
    endDate: new Date("2024-01-01"),
    status: "active",
  },
});
newCustomer.save();


*/
