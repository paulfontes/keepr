<script setup>
import { AppState } from '@/AppState.js';
import Example from '@/components/Example.vue';
import KeepCard from '@/components/KeepCards.vue';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const keeps = computed(() => AppState.keeps)

onMounted(() => {
  getKeeps()
})

async function getKeeps() {
  try {
    await keepsService.getKeeps()
  }
  catch (error) {
    Pop.error(error);
    logger.log(error)
  }
}
</script>

<template>
  <div class="container">
    <section class="row">
      <div class="masonry-container col-md-12 col-sm-6">
        <KeepCard />
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
.masonry-container {
  columns: 300px;

  >div {
    break-inside: avoid;
  }

}

@media(max-width:584px) {
  .masonry-container {
    columns: 130px;

    >div {
      break-inside: avoid;
    }

  }

}
</style>
