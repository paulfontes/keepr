export class VaultKeep {
    constructor(data) {
        this.id = data.id
        this.createdAt = new Date(this.createdAt)
        this.updatedAt = new Date(this.updatedAt)
        this.keepId = data.keepId
        this.vaultId = data.vaultId
        this.creatorId = data.creatorId
    }
}