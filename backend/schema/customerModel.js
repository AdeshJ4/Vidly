const mongoose = require("mongoose");
const Joi = require("joi");

const customerSchema = new mongoose.Schema({
  name: {
    type: String,
    required: true,
    trim: true,
    minlength: 4,
    maxlength: 50,
  },
  email: {
    type: String,
    required: true,
    unique: [true, "Email address already taken"],
    trim: true,
  },
  phone: {
    type: String,
    required: true,
    trim: true,
    unique: true,
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
      enum: ["bronze", "silver", "gold", "platinum"],
      default: "bronze",
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

function validateCustomer(customer) {
  const joiSchema = Joi.object({
    name: Joi.string().required().trim().min(5).max(50),
    email: Joi.string().required().trim().email(),
    phone: Joi.string()
      .required()
      .trim()
      .unique()
      .pattern(/^[0-9]+$/),
    dateOfBirth: Joi.date().required(),
    status: Joi.string().valid("active", "inactive").default("active"),
    address: Joi.object({
      street: Joi.string().required(),
      city: Joi.string().required(),
      state: Joi.string().required(),
      country: Joi.string().required(),
      postalCode: Joi.string()
        .length(6)
        .required()
        .pattern(/^[0-9]+$/),
    }).required(),
    membership: Joi.object({
      level: Joi.string()
        .valid("bronze", "silver", "gold", "platinum")
        .default("bronze"),
      startDate: Joi.date().default(() => new Date()),
      endDate: Joi.date(),
      status: Joi.string().valid("active", "expired").default("active"),
    }).required(),
  });

  return joiSchema.validate(customer);
}

const Customer = mongoose.model("Customer", customerSchema);

module.exports = { Customer, validateCustomer };

/*
{
    "name": "Adesh Jadhav",
    "email": "jadhavadesh13061@gmail.com",
    "phone": "9527370288",
    "dateOfBirth": "2001-06-22",
    "address": {
        "street": "123 Main St",
        "city": "Pune",
        "state": "CA",
        "postalCode": "123456",
        "country": "India"
    },
      "membership": {
    "level": "Gold",
    "endDate": "2024-07-30",
    "status": "active"
  }
}
*/
