<template>
    <div>
        <ol class="breadcrumb grey lighten-3 mt-3">
            <li class="breadcrumb-item">
                <a class="black-text" href="/Home">Home</a><i class="fas fa-angle-double-right mx-2"></i>
            </li>
            <li class="breadcrumb-item">
                <a :href="'/Category/Products/'+SlugUrl" class="black-text">{{CatalogName}}</a><i class="fas fa-angle-double-right mx-2"></i>
            </li>
            <li class="breadcrumb-item active">{{SlugUrl}}</li>
        </ol>

        <div class="row">
            <div class="col-lg-3 mb-3">
                <div id="filter-side" class="card card-body h-100">
                    <div v-if="SubCatalogs.length">
                        <h5>Related Categories</h5>
                        <ul>
                            <li v-for="(item,i) in SubCatalogs" :key="i">
                                <a :href="'/Category/Products/'+item.SlugUrl">{{item.CatalogName}}</a>
                            </li>
                        </ul>
                    </div>

                    <div v-if="Brands.length" class="my-3">
                        <h5 class="mb-2">Brand</h5>
                        <div v-for="(item, i) in Brands" :key="i" class="form-check">
                            <input :id="item.BrandId" type="checkbox" class="form-check-input">
                            <label :for="item.BrandId" class="form-check-label">{{item.Name}}</label>
                        </div>
                    </div>

                    <div v-if="Attributes.length" class="my-3">
                        <h5 class="mb-0">Attribute</h5>
                        <div v-for="item in Attributes" :key="item.AttributeId">
                            <p class="mt-2 mb-0 pl-2">{{item.KeyName}}</p>
                            <div v-for="(val, i) in item.Values" :key="i" class="form-check">
                                <input :id="i" type="checkbox" class="form-check-input">
                                <label :for="i" class="form-check-label">{{val}}</label>
                            </div>
                        </div>
                    </div>

                    <div v-if="Specifications.length" class="my-3">
                        <h5 class="mb-0">Specification</h5>
                        <div v-for="item in Specifications" :key="item.SpecificationId">
                            <p class="mt-2 mb-0 pl-2">{{item.KeyName}}</p>
                            <div v-for="(val, i) in item.Values" :key="i" class="form-check">
                                <input :id="i" type="checkbox" class="form-check-input">
                                <label :for="i" class="form-check-label">{{val}}</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-9">
                <div class="row">
                    <div v-for="(item,i) in data" :key="i" class="col-xl-3 col-lg-4 col-sm-6">
                        <div class="card hoverable h-100">
                            <div class="view overlay">
                                <img class="card-img-top" :src="item.ImageUrl" :alt="item.Name">
                                <a>
                                    <div class="mask rgba-white-slight"></div>
                                </a>
                            </div>
                            <div class="card-body pb-1">
                                <p class="product-name">{{item.Name}}</p>
          
                                <div class="pricing-area">
                                    <div class="p-price">
                                        <span>৳{{item.Price}}</span>
                                        <span v-if="item.OldPrice" class="p-discount d-block">৳{{item.OldPrice}}</span>
                                    </div>
                                    <div class="p-rating text-right">
                                        <div>
                                            <i :class="['fas', 'fa-star', { 'text-muted': !item.Rating}]"></i>
                                            <span :class="['rating-count', { 'text-muted': !item.RatingBy}]">{{item.RatingBy}}</span>
                                        </div>
                                        <span v-if="item.OldPrice" class="p-discount-percent">{{(100-((100*item.Price)/item.OldPrice)).toFixed(2)}}% off</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div> 
    </div>
</template>

<script>
    export default {
        props: ['slugUrl'],
        data() {
            return {
                CatalogName: "loading..",
                SlugUrl: "loading..",
                ParentCatalog: null,

                data: [],
                params: { Page: 2, PageSize: 4 },
                isLastPage: true,
                SubCatalogs: [],
                Brands: [],
                Attributes: [],
                Specifications:[]
            }
        },
        methods: {
            getData() {
                axios.get('/Category/GetProducts', { params: { slugUrl: this.slugUrl } }).then(response => {
                    console.log(response.data)
                    const { Breadcrumb, SubCatalogs, Products, Brands, Attributes, Specifications } = response.data;
                 
                    this.CatalogName = Breadcrumb.CatalogName;
                    this.SlugUrl = Breadcrumb.SlugUrl;
                    this.ParentCatalog = Breadcrumb.ParentCatalog;

                    this.data = Products;
                    this.SubCatalogs = SubCatalogs.length ? SubCatalogs: [];
                    this.Brands = Brands.length ? Brands: [];
                    this.Attributes = Attributes.length ? Attributes: [];
                    this.Specifications = Specifications.length ? Specifications: [];
                });
            },

            getDataOnDemand(params) {
                window.onscroll = () => {
                    if (!this.isLastPage) return;

                    const element = document.documentElement;
                    const bottomOfWindow = element.scrollTop + window.innerHeight === element.offsetHeight;
    
                    if (bottomOfWindow) {
                        axios.get('/Category/GetProducts', { params }).then(response => {
                            const { IsSuccess, Data } = response.data;
                            console.log(response)
                            this.isLastPage = IsSuccess;

                            if (!IsSuccess) return;

                            this.data.push(...Data.Results);
                            this.params.Page++;
                        });
                    }
                };
            },
        },
        beforeMount() {
            this.getData();
        },
        mounted() {
            //this.getDataOnDemand(this.params);
        }
    }
</script>