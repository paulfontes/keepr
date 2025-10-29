import { Vault } from '@/models/Vault.js'
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'
import { SavedKeep } from '@/models/Keep.js'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getMyVaults() {
    const response = await api.get('/account/vaults')
    const vaults = response.data.map((vault) => new Vault(vault))
    AppState.vaults = vaults
  }
}

export const accountService = new AccountService()
