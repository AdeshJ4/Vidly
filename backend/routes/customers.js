const express = require("express");
const customerRoute = express.Router();
const { Customer, validateCustomer } = require("../schema/customerModel");
const handleError = require("../utils/handleError");
const validateObjectId = require("../utils/validateObjectId");

// Get all customer, customers based on membership.
customerRoute.get("/", async (req, res) => {
  try {
    const { pageNumber = 1, pageSize = 10 } = req.query;

    const customers = await Customer.find()
      .skip((Number(pageNumber) - 1) * Number(pageSize))
      .limit(pageSize);

    const count = await Customer.countDocuments();

    return res.status(200).json({
      status: "success",
      count,
      data: customers,
    });
  } catch (error) {
    return handleError({ res, status: 500, error });
  }
});

customerRoute.get("/search", async (req, res) => {
  console.log("I am inside");
  try {
    const { name, pageNumber = 1, pageSize = 10 } = req.query;

    if (!name) {
      return handleError({
        res,
        message: "Name query parameter is required",
        status: 400,
      });
    }

    const query = { name: new RegExp(name, "i") };
    const customers = await Customer.find(query)
      .skip((Number(pageNumber) - 1) * Number(pageSize))
      .limit(Number(pageSize));

    const count = await Customer.countDocuments();

    return res.status(200).json({
      status: "success",
      count,
      data: customers,
    });
  } catch (error) {
    return handleError({ res, error });
  }
});

customerRoute.get("/membership", async (req, res) => {
  try {
    const { membership, pageNumber = 1, pageSize = 10 } = req.query;

    if (membership) {
      const validMemberships = ["bronze", "silver", "gold", "platinum"];
      if (!validMemberships.includes(membership.toLowerCase().trim())) {
        return handleError({
          res,
          message: `Invalid ${membership} membership level`,
          status: 400,
        });
      }
    } else {
      return handleError({ res, message: "Please provide membership" });
    }

    let query = { "membership.level": membership.toLowerCase().trim() };

    const customers = await Customer.find(query)
      .skip((Number(pageNumber) - 1) * Number(pageSize))
      .limit(pageSize);

    const count = await Customer.countDocuments();

    return res
      .status(200)
      .json({ status: "success", count: count, data: customers });
  } catch (error) {
    return handleError({ res, error });
  }
});

// get Single Customer
customerRoute.get("/:id", validateObjectId, async (req, res) => {
  try {
    const customer = await Customer.findById(req.params.id);
    if (!customer) {
      return handleError({
        res,
        message: `The customer with given id ${req.params.id} not found`,
        status: 400,
      });
    }
    return res.status(200).json({ status: "success", data: customer });
  } catch (error) {
    return handleError({ res, error, status: 500 });
  }
});

// create customer
customerRoute.post("/", async (req, res) => {
  try {
    const { error } = validateCustomer(req.body);
    if (error) {
      return handleError({
        res,
        message: error.details[0].message,
        status: 400,
      });
    }

    const customer = await Customer.create(req.body);
    return res.status(201).json({ status: "success", data: customer });
  } catch (error) {
    return handleError({ res, error, status: 400 });
  }
});

//Update Customer
customerRoute.put("/:id", validateObjectId, async (req, res) => {
  try {
    const { error } = validateCustomer(req.body);
    if (error) {
      return handleError({
        res,
        message: error.details[0].message,
        status: 400,
      });
    }

    const customer = await Customer.findByIdAndUpdate(req.params.id, req.body, {
      new: true,
    });

    return res.status(200).json({ status: "success", data: customer });
  } catch (error) {
    return handleError({ res, error, status: 500 });
  }
});

// delete Customer
customerRoute.delete("/:id", validateObjectId, async (req, res) => {
  try {
    const customer = await Customer.findByIdAndDelete(req.params.id);
    if (!customer) {
      return handleError({
        res,
        message: `The Customer with given id ${req.params.id} not found`,
        status: 400,
      });
    }

    return res.status(200).json({ status: "success", data: customer });
  } catch (error) {
    return handleError({ res, error, status: 500 });
  }
});

module.exports = customerRoute;
