import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Vault } from "@/models/Vault.js"
import { AppState } from "@/AppState.js"
import { Keep, SavedKeep } from "@/models/Keep.js"
import { vaultKeep } from "@/models/VaultKeep.js"

class VaultsService {
    async deleteVault(vaultId) {
        const response = await api.delete(`api/vaults/${vaultId}`)
        logger.log(`Vault ${vaultId} has been deleted! `)
    }
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
    async savedKeeps(vaultId) {
        AppState.savedKeeps = []
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        const savedKeeps = response.data.map((keep) => new SavedKeep(keep))
        AppState.savedKeeps = savedKeeps
    }
    // async getAllKeepsInPublicVault(vaultId) {
    //     const response = await api.get(`api.vaults/`)
    // }

}
export const vaultsService = new VaultsService()