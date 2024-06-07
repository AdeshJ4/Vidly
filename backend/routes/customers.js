const express = require("express");
const customerRoute = express.Router();
const { Customer, validateCustomer } = require("../schema/customerModel");
const handleError = require("../utils/handleError");
const validateObjectId = require("../utils/validateObjectId");

customerRoute.get("/", async (req, res) => {
  try {
    const customers = await Customer.find();
    return res.status(200).json({ status: "success", data: customers || [] });
  } catch (error) {
    return handleError({ res, status: 500, error });
  }
});

customerRoute.post("/", async (req, res) => {
  try {
    const { value, error } = validateCustomer(req.body);
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
