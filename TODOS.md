Disorganized Notes on Necessary Changes
=======================================


tile selection for edit mode - needs to use raycasting into tiles.

tile drag, pretty good ... but may need some refining

test ipad touch interface with unity remote

refine map height some:
use distance to sea algorithm
use jump at sea / jump at peaks, mostly low/smooth otherwise.

create tile image for  the 50 belt types
- created 10 base images which can rotate and reverse shader direction.

DLL logic so belt connections link up to nearby entry/exit points.

finish a sample rule set, create model representation of rule set

Replace FacilityTypeInfo logic with tables ( instead of introspection )

Break Facility class into class tree: BeltFacility, MakeFacility, EditFacility,VendFacility.

Site + Event refer to the same thing, figure out best way to use terms:
    facility -> site ?   keep event ?


Organize to answer these questions, consider: emulation, editing, drawing
When placing tile, what are adjacent connections? ( looking north, does it link south? )
Multi-tile facilities - each tile knows the SAME facility, but has in index into its sub-map.
    By 'Same' facility, I mean all are using a single object for emulation;
    but separate descriptors for each 'indexed' submap element.
Hmm, could describe each BELT as two to four sub-elements, each moving 50% of distance.

Thinking:
Can ask Belt 'what is your south link', but a larger shape needs to know where we are asking from.
OR:
Can we attach link information to a 'link map', so when a facility is placed it marks its open links.

MultiTileFacility - wraps a single Facility object, but knows the sub-part ( eg. tile ) and rotation.




Tiers for Make + Vend facilities:
1) I want speed to be managed by some clobal facilities ( probably 'management' types ).
    so speed can improve by building and funding management
2) I would like for make/vend facilities to cost more for higher tier products
    one way was to create multiple make/vend facility types, another is to just raise cost
    based on the event tier, a third would be to charge more for each aditional facility.