<template>
  <div>
    <svg-icon icon-class="question" @click="goto"/>
  </div>
</template>

<script>
export default {
  name: 'ZRDoc',
  data() {
    return {
      url: 'https://alidocs.dingtalk.com/document/edit?dentryKey=pD93v0VrwCeWG5LN&docKey=5VLqXQ5KkvjYOX19&mainsiteOrigin=mainsite&rnd=0.6115105748562775&scene=universalSpace&type=d'
    }
  },
  methods: {
    goto() {
      window.open(this.url)
    }
  }
}
</script>
