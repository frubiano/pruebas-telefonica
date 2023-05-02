# pruebas-telefonica

Back End desarrollado en C# - Net 6 usando entity framework core

Proyecto: Ahorra- market
Descripción: Este proyecto es una aplicación web que permite informar  a nuestros usuarios en que almacenes o tiendas de cadena, 
los articulos de la canasta familiar estan mas baratos

Equipo: Este proyecto es desarrollado por un sola una persona la cual es Felipe es el programador y encargado  de la interfa grafica.

Estado actual: El proyecto se encuentra en su fase inicial de desarrollo. La interfaz gráfica y la funcionalidad básica de la aplicación la cual 
es el CRUD este ya han sido implementado.

En resumen, nuestro proyecto Ahorra- market es una aplicación web en desarrollo quese creo  con la necesita de que las personas se informen  
en que almacenes o tiendas de cadena, los articulos de la canasta familiar estan mas baratos ya que es inebitable que a cada instante los articulos
vayan  en incremento  su precio ya se ha del 10% hasta el 20%. Con este proyecto prentendo beneficiar a las usuarios que deseeen  compara
la variedad de precios que nos ofrecen los almacenes de cadena de un mismo producto  y asi  mismo  tener en cuanta cuanto nos
podemos ahorrar haciendo  mercardo en los almacenes de cadena el cual su precio es mas barato. 


Consultas
 * Conocer en que tienda y que tipo de producto su precio es mas barato:
SELECT B.nombre 'Producto', C.nombre 'Tienda', (A.precio) as precio FROM producto_tienda A
INNER JOIN producto B ON B.id_producto = A.id_producto INNER JOIN tienda C ON C.id_tienda = A.id_tienda  where b.nombre like '%' order by precio  asc

* conocer en que tieda  el producto  PAN y LECHE su precio es mas caro:
SELECT B.nombre 'Producto', C.nombre 'Tienda', A.precio FROM producto_tienda A
INNER JOIN producto B ON B.id_producto = A.id_producto INNER JOIN tienda C ON C.id_tienda = A.id_tienda  where B.nombre in ('PAN','LECHE')order by precio  asc

*Conocer caules son los producto que tiene en  la tienda Surtimax :
SELECT B.nombre 'Producto', C.nombre 'Tienda'   from  producto_tienda 
AINNER JOIN producto B ON B.id_producto = A.id_producto INNER JOIN tienda C ON C.id_tienda = A.id_tienda where C.nombre ='Surtimax'  group  by  B.nombre, C.nombre 

*consulta que me devuelva en todo aquel producto y la tienda a que pertenece el cual  que su precio  se ha mayor a $10000
select producto.nombre ,tienda.nombre as tienda , producto_tienda.precio from tienda,  producto, producto_tienda
where producto.id_producto= producto_tienda.id_producto and tienda.id_tienda= producto_tienda.id_tienda 
group  by producto.nombre , producto_tienda.precio, tienda.nombre  HAVING precio >10000;

