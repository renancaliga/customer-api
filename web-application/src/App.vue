<template>
  <div id="app">

    <nav>
      <div class="nav-wrapper blue darken-1">
        <a href="#" class="brand-logo center">Clientes</a>
      </div>
    </nav>

    <div class="container">

      <ul>
        <li v-for="(erro, index) of erros" :key="index">
          <b>{{ erro }}</b>
        </li>
      </ul>

      <form @submit.prevent="create">

        <label>Nome</label>
        <input type="text" placeholder="Nome" v-model="customer.name">
        <label>Porte</label>
        <input type="text" placeholder="INFORME APENAS GRANDE, MEDIA OU PEQUENA" v-model="customer.size">

        <button class="waves-effect waves-light btn-small">Salvar<i class="material-icons left">save</i></button>

      </form>

      <table>

        <thead>

          <tr>
            <th>ID</th>
            <th>NOME</th>
            <th>PORTE</th>

          </tr>

        </thead>

        <tbody>

          <tr v-for="customer in customers" :key="customer.id">

            <td>{{ customer.id }}</td>
            <td>{{ customer.name }}</td>
            <td>{{ customer.size }}</td>
            <td>
              <button @click="update(customer)" class="waves-effect btn-small blue darken-1"><i
                  class="material-icons">create</i></button>
              <button @click="remove(customer)" class="waves-effect btn-small red darken-1"><i
                  class="material-icons">delete_sweep</i></button>
            </td>

          </tr>

        </tbody>

      </table>

    </div>

  </div>
</template>

<script>

import Customer from './services/customers'

export default {
  data() {
    return {
      customer: {
        id: '',
        name: '',
        size: ''
      },
      customers: [],
      erros: ""
    }
  },
  mounted() {
    this.getCustomers();
  },
  methods: {

    getCustomers() {
      Customer.getCustomers().then(response => {
        this.customers = response.data;
      })
    },

    create() {

      if (!this.customer.id) {

        this.customer = {
          name: this.customer.name,
          size: this.customer.size
        }

        Customer.createCustomer(this.customer).then(response => {
          this.customer = {};
          this.response = response;

          alert('Cliente salvo com sucesso!')
          this.getCustomers();
          this.erros = "";
        }).catch(e => {
          console.log(e.response)
          this.erros = e.response;
        })

      } else {


        Customer.updateCustomer(this.customer.id, this.customer).then(response => {
          this.customer = {};
          this.response = response;
          alert('Cliente atualizado com sucesso!')
          this.getCustomers();
          this.erros = [];
        })

      }

    },

    update(customer) {

      this.customer = customer;

    },

    remove(customer) {

      if (confirm('Deseja excluir o registro?')) {
        customer.id = parseInt(customer.id, 10);
        Customer.deleteCustomer(customer.id).then(response => {
          this.response = response;
          alert('Cliente removido com sucesso!');
          this.getCustomers();
          this.erros = [];
        }).catch(e => {
          this.erros = e.response.data;
        })
      }

    }
  }
}
</script>

<style></style>
