import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { SavedKeep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"

class VaultKeepsService {
    async createVaultKeep(activeKeep) {
        const response = await api.post(`api/vaultkeeps`, activeKeep)
        const savedKeep = new SavedKeep(response.data)
        AppState.savedKeeps.push(savedKeep)
    }
    async deleteVaultKeep(vaultKeepId) {
        logger.log(vaultKeepId)
        // const response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        // logger.log('DELETED KEEP', response.data)
    }

}
export const vaultKeepsService = new VaultKeepsService()