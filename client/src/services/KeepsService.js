import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"

class KeepsService {
    async deleteKeep(keepId) {
        logger.log(keepId)
        // const response = await api.delete(`/api/keeps/${keepId}`)
        // logger.log('DELETED KEEP', response.data)
    }
    async getKeeps() {
        AppState.keeps = []
        const response = await api.get('api/keeps')
        // logger.log(response.data)
        this.keepHandler(response)
    }
    async getKeepById(keepId) {
        const response = await api.get(`api/keeps/${keepId}`)
        // logger.log(response.data)
        AppState.activeKeep = new Keep(response.data)
        // AppState.activeKeep.views++


    }
    async createKeep(keepData) {
        const response = await api.post('api/keeps', keepData)
        const keep = new Keep(response.data)
        AppState.keeps.unshift(keep)
    }

    async keepHandler(response) {
        const keeps = response.data.map((keep) => new Keep(keep))
        AppState.keeps = keeps

    }
}
export const keepsService = new KeepsService()