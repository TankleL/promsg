
#ifndef $(PROTOCOL_NAME)
#define $(PROTOCOL_NAME)

$(PROTOCOL_REF_HEADERS)
#include <msgpack.hpp>

$(PARAM_LIST)

namespace msgpack
{
	MSGPACK_API_VERSION_NAMESPACE(MSGPACK_DEFAULT_API_NS)
	{
		namespace adaptor
		{
			$(AS_METHODS)
		} // namespace adaptor
	} // MSGPACK_API_VERSION_NAMESPACE(MSGPACK_DEFAULT_API_NS)
} // namespace msgpack

#endif // $(PROTOCOL_NAME)
