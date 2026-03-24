<template>
  <div>
    <svg-icon icon-class="github" @click="goto"/>
  </div>
</template>

<script>
export default {
  name: 'IZoryGit',
  data() {
    return {
      url: 'http://172.18.100.137:7080/users/sign_in'
    }
  },
  methods: {
    goto() {
      window.open(this.url)
    }
  }
}
</script>
