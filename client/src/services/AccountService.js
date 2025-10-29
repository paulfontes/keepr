import { Vault } from '@/models/Vault.js'
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'
import { SavedKeep } from '@/models/Keep.js'

class AccountService {
  async updateAccount(updateData) {
    const response = await api.put('/account', updateData)
    const updatedData = new Account(response.data)
    AppState.account = updateData
  }
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getMyVaults() {
    const response = await api.get('account/vaults')
    // logger.log(response.data)
    const myVaults = response.data.map((vault) => new Vault(vault))
    AppState.myVaults = myVaults
  }
}

export const accountService = new AccountService()
