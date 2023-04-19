import { http } from './config'


export default {

    getCustomers: () => {
        return http.get('Customer')
    },

    createCustomer: (customer) => {
        return http.post('Customer', customer)
    },

    updateCustomer: (customerId, customer) => {
        return http.put(`Customer/${customerId}`, customer)
    },

    deleteCustomer: (customerId) => {
        return http.delete(`Customer/${customerId}`)
    }
}