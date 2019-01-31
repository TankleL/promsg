struct $(PARAM_NAME)
{
	// default constructor
	$(PARAM_NAME)() = delete;
	
	// move constructor
	$(PARAM_NAME)($(PARAM_NAME)&&) = default;
	
	// copy constructor
	$(CONSTRUCTOR_COPY)
		
	// protocol properties
	$(PROPERTIES)
	
	MSGPACK_DEFINE($(PROPERTIES_LIST));
};
