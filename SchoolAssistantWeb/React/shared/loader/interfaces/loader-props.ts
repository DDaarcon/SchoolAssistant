import LoaderSize from "../enums/loader-size";
import LoaderType from "../enums/loader-type";

type LoaderProps = {
    enable?: boolean,
    type?: LoaderType;
    size?: LoaderSize;
    className?: string;
}
export default LoaderProps;