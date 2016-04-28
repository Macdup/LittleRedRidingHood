// On récupère le documentActif.
var document = app.activeDocument;

var root = new XML("<document.name/>");

for(var i  = 0; i < document.layers.length ; i++){
        
        $.writeln(document.layers[i]);
        var layer = document.layers[i];
        var layerXml = new XML("<" + document.layers[i].name + "/>");
        root.appendChild(layerXml);
        
         for(var j  = 0; j < layer.pathItems.length ; j++){
                   var pathItem =  layer.pathItems[j];
                   var pathItemXml = new XML("<PathItem/>");
				   for(var k  = 0; k < pathItem.pathPoints.length ; k++){
						var pathPoint = pathItem.pathPoints[k];
						var pathPointXml = new XML("<PathPoint/>");
						pathPointXml.@x = pathPoint.anchor[0];
						pathPointXml.@y = pathPoint.anchor[1];
						pathItemXml.appendChild(pathPointXml);
				   }
                   layerXml.appendChild(pathItemXml);
             }
         
    }



//On récupère son nom pour en faire le nom du xml sortie.
var file = new File("~/Desktop/" + document.name +".xml");
var xml = root.toXMLString();
file.open("W");
file.write(xml);
file.close();