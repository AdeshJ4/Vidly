const morgan = require('morgan');
const cors = require('cors');
const customerRoute = require('../routes/customers');

module.exports = function (app, express) {
    app.use(morgan('dev'));
    app.use(cors());
    app.use(express.json());

    app.use('/api/customers', customerRoute);
}