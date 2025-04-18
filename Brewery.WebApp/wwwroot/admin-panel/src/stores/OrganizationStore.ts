import { makeAutoObservable } from 'mobx'
import axios from 'axios'

class OrganizationStore {
  organizations: { id: string; name: string; addresses: string[] }[] = []

  constructor() {
    makeAutoObservable(this)
  }

  async loadOrganizations() {
    axios.defaults.baseURL = 'http://localhost:5029';

    const res = await axios.get('/api/organizations')
    this.organizations = res.data
  }
}

const store = new OrganizationStore()
export default store
