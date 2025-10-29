import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,

  /** @type {import('./models/Account.js').Profile} user info from the database*/
  profile: null,


  /** @type {import('./models/Keep.js').Keep[]} user info from the database*/
  keeps: [],
  /** @type {import('./models/Keep.js').Keep} */
  activeKeep: null,

  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  /** @type {import('./models/VaultKeep.js').vaultKeep[]} */
  vaultsKeeps: [],

  /** @type {import('./models/Vault.js').Vault} */
  vaultKeep: null,

  /** @type {import('./models/Keep.js').SavedKeep[]} */
  savedKeeps: [],


})

