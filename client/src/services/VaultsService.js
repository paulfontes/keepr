import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Vault } from "@/models/Vault.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"

class VaultsService {
    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        const vault = new Vault(response.data)
        AppState.vaults.unshift(vault)
    }
    async getVaultById(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}`)
        const vault = new Vault(response.data)
        AppState.vaultKeep = vault
    }
    async getKeepsInVault(vaultId) {
        AppState.keeps = []
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        const keeps = response.data.map((keep) => new Keep(keep))
        AppState.keeps = keeps

    }

}
export const vaultsService = new VaultsService()