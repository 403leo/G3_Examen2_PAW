//using Microsoft.AspNetCore.Razor.TagHelpers;
//using PAWMartes.Services;

//namespace PAWMartes.PAWTagHelper
//{
//    [HtmlTargetElement("carrito-resumen")]
//    public class ResumenCarrito : TagHelper
//    {
//        public readonly ICarritoServices _carrito;

//        public ResumenCarrito(ICarritoServices carrito)
//        {
//            _carrito = carrito;
//        }

//        public override void Process(TagHelperContext context, TagHelperOutput output)
//        {
//            var total = _carrito.ObtenerTotal();
//            output.TagName = "div"; // Reemplaza el nombre de la etiqueta
//            // Un link hacia el carrito
//            output.Content.SetHtmlContent(
//                $@"<a href='/Carrito' class='cart-link'>
//                     <i class='fa fa-shopping-cart'></i> ({total})
//                    </a>"
//                   );
//        }

//        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
//        {
//            var total = _carrito.ObtenerTotal();
//            output.TagName = "div"; // Reemplaza el nombre de la etiqueta
//            // Un link hacia el carrito
//            output.Content.SetHtmlContent(
//                $@"<a href='/Carrito' class='cart-link'>
//                     <i class= 'fa fa-shopping-cart'></i> ({total})
//                    </a>"
//                   );
//            return base.ProcessAsync(context, output);
//        }
//    }
//}
