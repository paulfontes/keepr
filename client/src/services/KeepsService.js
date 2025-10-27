import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Keep } from "@/models/Keep.js"

class KeepsService {
    async getKeeps() {
        AppState.keeps = []
        const response = await api.get('api/keeps')
        logger.log(response.data)
        this.keepHandler(response)
    }

    async keepHandler(response) {
        const keeps = response.data.map((keep) => new Keep(keep))
        AppState.keeps = keeps

    }
}
export const keepsService = new KeepsService()