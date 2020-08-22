<template>
    <div id="home" class="bg-primary">
        <h1>Home -Vue</h1>
        <input class="datepicker" v-model="date" />
        {{date}}
    </div>
</template>
<script>
  
    export default {
        data() {
            return {
                date:""
            }
        },
        mounted() {
            var self = this; // the vue instance
            $('.datepicker').pickadate({
                onSet: function (context) {
                    self.date = context
                }
            })
        }
    };
</script>